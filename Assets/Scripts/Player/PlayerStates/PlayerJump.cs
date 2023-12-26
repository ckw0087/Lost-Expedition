using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private int maxJumps = 2;

    private bool jumpPerformed;
    private int jumpAnimatorParameter = Animator.StringToHash("Jumping");
    private int doubleJumpParameter = Animator.StringToHash("DoubleJump");
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

    // Fusion network update
    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<PlayerNetworkData>(Object.InputAuthority, out var input))
        {
            Jump(input);
        }
    }

    public PlayerNetworkData GetPlayerNetworkInput()
    {
        PlayerNetworkData data = new PlayerNetworkData();
        data.JumpPressed = jumpPerformed;

        return data;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        jumpPerformed = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        jumpPerformed = false;
    }

    //protected override void GetInput()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Jump();
    //    }
    //}

    private void Jump(PlayerNetworkData input)
    {
        if (input.JumpPressed)
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
        jumpAction.canceled += OnJumpCanceled;
    }

    private void OnDisable()
    {
        Jumper.OnJump -= JumpResponse;

        // Unsubscribe jump action event
        jumpAction.Disable();
        jumpAction.performed -= OnJumpPerformed;
        jumpAction.canceled -= OnJumpCanceled;
    }
}
