using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterMechanics
{
    [SerializeField] private float moveSpeed = 5f;

    public float MoveSpeed { get; set; }

    private readonly int movingParameter = Animator.StringToHash("Moving");

    protected override void Start()
    {
        base.Start();

        MoveSpeed = moveSpeed;
    }
    protected override void HandleAbility()
    {
        base.HandleAbility();

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
