using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float speed = 10f;

    private float horizontalMovement;
    private float movement;

    //private int idleAnimatorParameter = Animator.StringToHash("Idle");
    //private int runAnimatorParameter = Animator.StringToHash("Run");


    protected override void InitState()
    {
        base.InitState();
    }

    public override void ExecuteState()
    {
        MovePlayer();
    }

    // Moves our Player    
    private void MovePlayer()
    {
        if (Mathf.Abs(horizontalMovement) > 0.1f)
        {
            movement = horizontalMovement;
        }
        else
        {
            movement = 0f;
        }

        float moveSpeed = movement * speed;
        playerController.SetHorizontalForce(moveSpeed);
    }

    // Initialize our internal movement direction   
    protected override void GetInput()
    {
        horizontalMovement = horizontalInput;
    }

    //public override void SetAnimation()
    //{
    //    animator.SetBool(idleAnimatorParameter, horizontalMovement == 0 && playerController.Conditions.IsCollidingBelow);
    //    animator.SetBool(runAnimatorParameter, Mathf.Abs(horizontalInput) > 0.1f && playerController.Conditions.IsCollidingBelow);
    //}
}
