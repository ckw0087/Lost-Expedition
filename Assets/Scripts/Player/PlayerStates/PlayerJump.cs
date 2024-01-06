using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private int maxJumps = 2;

    private int jumpAnimatorParameter = Animator.StringToHash("Jumping");
    private int doubleJumpAnimatorParameter = Animator.StringToHash("DoubleJumping");
    private int fallAnimatorParameter = Animator.StringToHash("Falling");

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

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.LevelCompleted)
        {
            return;
        }

        Jump();
    }

    //protected override void GetInput()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Jump();
    //    }
    //}

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
        SoundManager.Instance.PlaySound(AudioLibrary.Instance.JumpClip);
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
                                                  && JumpsLeft > 0
                                                  && !playerController.Conditions.IsFalling
                                                  && !playerController.Conditions.IsJetpacking);

        // Double jump
        animator.SetBool(doubleJumpAnimatorParameter, playerController.Conditions.IsJumping
                                                  && !playerController.Conditions.IsCollidingBelow
                                                  && JumpsLeft == 0
                                                  && !playerController.Conditions.IsFalling
                                                  && !playerController.Conditions.IsJetpacking);

        // Fall
        animator.SetBool(fallAnimatorParameter, playerController.Conditions.IsFalling
                                                  && playerController.Conditions.IsJumping
                                                  && !playerController.Conditions.IsCollidingBelow
                                                  && !playerController.Conditions.IsJetpacking);
    }

    private void JumpResponse(float jump)
    {
        playerController.SetVerticalForce(Mathf.Sqrt(2f * jump * Mathf.Abs(playerController.Gravity)));
    }

    private void OnEnable()
    {
        Jumper.OnJump += JumpResponse;

        // Subscribe jump action event
        jumpAction.Enable();
        jumpAction.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        Jumper.OnJump -= JumpResponse;

        // Unsubscribe jump action event
        jumpAction.Disable();
        jumpAction.performed -= OnJumpPerformed;
    }
}
