using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private int maxJumps = 2;

    private int jumpAnimatorParameter = Animator.StringToHash("Jumping");
    //private int doubleJumpParameter = Animator.StringToHash("DoubleJump");
    private int fallAnimatorParameter = Animator.StringToHash("Falling");
    private int landAnimatorParameter = Animator.StringToHash("Landing");

    // Return how many jumps we have left
    public int JumpsLeft { get; set; }

    protected override void InitState()
    {
        base.InitState();
        JumpsLeft = maxJumps;
    }

    public override void ExecuteState()
    {
        if (playerController.Conditions.IsCollidingBelow && playerController.Force.y == 0f)
        {
            JumpsLeft = maxJumps;
            playerController.Conditions.IsJumping = false;
        }
    }

    protected override void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!CanJump())
        {
            return;
        }

        if (JumpsLeft == 0)
        {
            return;
        }

        JumpsLeft -= 1;

        float jumpForce = Mathf.Sqrt(jumpHeight * 2f * Mathf.Abs(playerController.Gravity));
        playerController.SetVerticalForce(jumpForce);
        playerController.Conditions.IsJumping = true;
    }

    private bool CanJump()
    {
        if (!playerController.Conditions.IsCollidingBelow && JumpsLeft <= 0)
        {
            return false;
        }

        if (playerController.Conditions.IsCollidingBelow && JumpsLeft <= 0)
        {
            return false;
        }

        return true;
    }

    public override void SetAnimation()
    {
        // Jump
        animator.SetBool(jumpAnimatorParameter, playerController.Conditions.IsJumping
                                                  && !playerController.Conditions.IsCollidingBelow
                                                  && JumpsLeft == 0
                                                  && !playerController.Conditions.IsFalling
                                                  && !playerController.Conditions.IsJetpacking);

        //// Double jump
        //animator.SetBool(doubleJumpParameter, playerController.Conditions.IsJumping
        //                                          && !playerController.Conditions.IsCollidingBelow
        //                                          && JumpsLeft == 0
        //                                          && !playerController.Conditions.IsFalling
        //                                          && !playerController.Conditions.IsJetpacking);

        // Fall
        animator.SetBool(fallAnimatorParameter, playerController.Conditions.IsFalling
                                                  && playerController.Conditions.IsJumping
                                                  && !playerController.Conditions.IsCollidingBelow
                                                  && !playerController.Conditions.IsJetpacking);

        //// Land
        //animator.SetBool(landAnimatorParameter, playerController.Conditions.IsCollidingBelow
        //                                          && playerController.Conditions.IsJumping
        //                                          && !playerController.Conditions.IsFalling 
        //                                          && !playerController.Conditions.IsJetpacking);
    }

    private void JumpResponse(float jump)
    {
        playerController.SetVerticalForce(Mathf.Sqrt(2f * jump * Mathf.Abs(playerController.Gravity)));
    }

    private void OnEnable()
    {
        Jumper.OnJump += JumpResponse;
    }

    private void OnDisable()
    {
        Jumper.OnJump -= JumpResponse;
    }
}
