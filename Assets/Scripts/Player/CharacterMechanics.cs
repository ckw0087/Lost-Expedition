using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMechanics : MonoBehaviour
{
    protected float horizontalInput;
    protected float verticalInput;
    protected Vector2 movementInput;

    protected MyCharacterController myCharacterController;
    protected CharacterMovement characterMovement;
    protected Character character;
    protected Animator animator;

    private PlayerInput playerInput;
    private InputAction moveAction;

    protected virtual void Awake()
    {
        playerInput = GetComponent<PlayerInput>();       

        moveAction = playerInput.actions["Move"];
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        myCharacterController = GetComponent<MyCharacterController>();
        characterMovement = GetComponent<CharacterMovement>();
        character = GetComponent<Character>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleAbility();
    }

    protected virtual void OnEnable()
    {
        moveAction.Enable();
    }
    
    protected virtual void OnDisable()
    {
        moveAction.Disable();
    }

    protected virtual void HandleAbility()
    {
        movementInput = moveAction.ReadValue<Vector2>();
        InternalInput();
    }

    protected virtual void HandleInput()
    {

    }

    protected virtual void InternalInput()
    {
        if (character.CharacterTypes == Character.CharacterType.Player)
        {
            horizontalInput = movementInput.x;
            verticalInput = movementInput.y;
        }      
    }
}
