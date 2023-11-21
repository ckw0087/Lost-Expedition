using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : CharacterMechanics
{
    [SerializeField] private float moveSpeed = 5f;

    public float MoveSpeed { get; set; }

    private readonly int movingParameter = Animator.StringToHash("Moving");
    private InputAction moveAction;

    protected override void Awake()
    {
        base.Awake();

        moveAction = playerInput.actions["Move"];
    }

    protected override void Start()
    {
        base.Start();

        MoveSpeed = moveSpeed;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        moveAction.Enable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        moveAction.Disable();
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();

        movementInput = moveAction.ReadValue<Vector2>();

        MoveCharacter();
        UpdateAnimation();
    }

    private void MoveCharacter()
    {
        Vector2 movement = new Vector2(x: horizontalInput, y: verticalInput);

        //If we move in diagonally, e.g pressing A & W together, same 1 unit has been moved
        Vector2 movementNormalized = movement.normalized;

        Vector2 movementSpeed = movementNormalized * MoveSpeed;
        myCharacterController.SetMovement(movementSpeed);
    }

    private void UpdateAnimation()
    {
        if (character.CharacterTypes == Character.CharacterType.Player)
        {
            if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
            {
                if (character.CharacterAnimator != null)
                {
                    character.CharacterAnimator.SetBool(movingParameter, true);
                }
            }
            else
            {
                if (character.CharacterAnimator != null)
                {
                    character.CharacterAnimator.SetBool(movingParameter, false);
                }
            }
        }
    }
}
