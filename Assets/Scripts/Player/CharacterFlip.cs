using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : CharacterMechanics
{
    [SerializeField] private float threshold = 0.1f;

    public bool FacingRight { get; set; }

    protected override void Awake()
    {
        base.Awake();

        FacingRight = true;
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();

        if (myCharacterController.CurrentMovement.normalized.magnitude > threshold)
        {
            if (myCharacterController.CurrentMovement.normalized.x > 0)
            {
                FaceDirection(1);
            }
            else
            {
                FaceDirection(-1);
            }
        }
    }

    // Makes our character face the direction in which is moving
    private void FaceDirection(int newDirection)
    {
        if (newDirection == 1)
        {
            character.CharacterSprite.transform.localScale = new Vector3(1, 1, 1);
            FacingRight = true;
        }
        else
        {
            character.CharacterSprite.transform.localScale = new Vector3(-1, 1, 1);
            FacingRight = false;
        }
    }
}
