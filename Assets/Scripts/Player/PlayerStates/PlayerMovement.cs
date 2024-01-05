using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float speed = 10f;

    public float Speed { get; set; }
    public float InitialSpeed => speed;

    private float horizontalMovement;
    private float movement;

    private int idleAnimatorParameter = Animator.StringToHash("Idling");
    private int runAnimatorParameter = Animator.StringToHash("Running");

    protected override void InitState()
    {
        base.InitState();
        Speed = speed;
    }

    public override void ExecuteState()
    {
        MovePlayer();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // Handle movement input
        Vector2 input = context.ReadValue<Vector2>();
        horizontalMovement = input.x;
        horizontalInput = input.x;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Handle movement stopping
        Vector2 input = context.ReadValue<Vector2>();
        horizontalMovement = input.x;
        horizontalInput = input.x;
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

        float moveSpeed = movement * Speed;
        moveSpeed = EvaluateExternalForces(moveSpeed);

        playerController.SetHorizontalForce(moveSpeed);
    }

    //// Initialize our internal movement direction   
    //protected override void GetInput()
    //{
    //    horizontalMovement = horizontalInput;
    //}

    public override void SetAnimation()
    {
        animator.SetBool(idleAnimatorParameter, horizontalMovement == 0 && playerController.Conditions.IsCollidingBelow);
        animator.SetBool(runAnimatorParameter, Mathf.Abs(horizontalInput) > 0.1f && playerController.Conditions.IsCollidingBelow);
    }

    private float EvaluateExternalForces(float moveSpeed)
    {
        if (playerController.Friction > 0)
        {
            moveSpeed = Mathf.Lerp(playerController.Force.x, moveSpeed, Time.deltaTime * 10f * playerController.Friction);
        }

        return moveSpeed;
    }

    private void OnEnable()
    {
        // Subscribe move action event
        moveAction.Enable();
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        // Unsubscribe move action event
        moveAction.Disable();
        moveAction.performed -= OnMovePerformed;
        moveAction.canceled -= OnMoveCanceled;
    }
}
