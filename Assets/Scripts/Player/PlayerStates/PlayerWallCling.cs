using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallCling : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float fallFactor = 0.5f;

    protected override void GetInput()
    {
        if (horizontalInput <= -0.1f || horizontalInput >= 0.1f)
        {
            WallCling();
        }
    }

    public override void ExecuteState()
    {
        ExitWallCling();
    }

    private void WallCling()
    {
        if (playerController.Conditions.IsCollidingBelow || playerController.Force.y >= 0) //on the FLOOR or in the AIR
        {
            return;
        }

        if (playerController.Conditions.IsCollidingLeft && horizontalInput <= -0.1f ||
            playerController.Conditions.IsCollidingRight && horizontalInput >= 0.1f)
        {
            playerController.SetWallClingMultiplier(fallFactor);
            playerController.Conditions.IsWallClinging = true;
        }
    }

    private void ExitWallCling()
    {
        if (playerController.Conditions.IsWallClinging)
        {
            if (playerController.Conditions.IsCollidingBelow || playerController.Force.y >= 0)
            {
                playerController.SetWallClingMultiplier(0f);
                playerController.Conditions.IsWallClinging = false;
            }

            if (playerController.FacingRight)
            {
                if (horizontalInput <= -0.1f || horizontalInput < 0.1f)
                {
                    playerController.SetWallClingMultiplier(0f);
                    playerController.Conditions.IsWallClinging = false;
                }
            }
            else
            {
                if (horizontalInput >= 0.1f || horizontalInput > -0.1f)
                {
                    playerController.SetWallClingMultiplier(0f);
                    playerController.Conditions.IsWallClinging = false;
                }
            }
        }
    }
}
