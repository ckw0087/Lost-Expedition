using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRoll : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float rollSpeed = 10f;
    [SerializeField] private float rollDuration = 1f;

    private bool isRolling;

    private int rollAnimatorParameter = Animator.StringToHash("Rolling");

    protected override void InitState()
    {
        base.InitState();
    }

    public override void ExecuteState()
    {
        if (isRolling)
        {
            playerController.SetHorizontalForce(rollSpeed);
        }
    }

    private void OnRollPerformed(InputAction.CallbackContext context)
    {
        if (!CanRoll()) return;

        StartCoroutine(DoRoll());
    }

    private bool CanRoll()
    {
        if (!playerController.Conditions.IsJumping 
            && !playerController.Conditions.IsFalling 
            && playerController.Conditions.IsCollidingBelow)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator DoRoll()
    {
        isRolling = true;

        yield return new WaitForSeconds(rollDuration);

        isRolling = false;
    }

    public override void SetAnimation()
    {
        animator.SetTrigger(rollAnimatorParameter);
    }

    private void OnEnable()
    {
        // Subscribe move action event
        rollAction.Enable();
        rollAction.performed += OnRollPerformed;
    }

    private void OnDisable()
    {
        // Unsubscribe move action event
        rollAction.Disable();
        rollAction.performed -= OnRollPerformed;
    }
}
