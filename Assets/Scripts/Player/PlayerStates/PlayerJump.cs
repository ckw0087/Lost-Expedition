using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private int maxJumps = 2;

    protected override void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        float jumpForce = Mathf.Sqrt(jumpHeight * 2f * Mathf.Abs(playerController.Gravity));
        playerController.SetVerticalForce(jumpForce);
    }
}
