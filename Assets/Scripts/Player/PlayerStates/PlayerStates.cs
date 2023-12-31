using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStates : MonoBehaviour
{
    protected PlayerController playerController;
    protected Animator animator;   
    protected float horizontalInput;
    protected float verticalInput;

    protected PlayerInput playerInput;
    protected InputAction moveAction;
    protected InputAction jumpAction;
    protected InputAction rollAction;

    protected virtual void Awake()
    {
        // Get the PlayerInput component attached to the same GameObject
        playerInput = GetComponent<PlayerInput>();

        // Retrieve the actions from the Input Action Asset
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        rollAction = playerInput.actions["Roll"];
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        InitState();
    }

    // Here we call some logic we need in start
    protected virtual void InitState()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    // Override in order to create the state logic
    public virtual void ExecuteState()
    {

    }

    // Gets the normal Input   
    //public virtual void LocalInput()
    //{
    //    horizontalInput = Input.GetAxisRaw("Horizontal");
    //    verticalInput = Input.GetAxisRaw("Vertical");

    //    GetInput();
    //}

    // Override to support other Inputs
    protected virtual void GetInput()
    {

    }

    public virtual void SetAnimation()
    {

    }
}
