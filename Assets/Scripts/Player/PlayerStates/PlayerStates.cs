using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    protected PlayerController playerController;
    protected float horizontalInput;
    protected float verticalInput;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        InitState();
    }

    // Here we call some logic we need in start
    protected virtual void InitState()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Override in order to create the state logic
    public virtual void ExecuteState()
    {

    }

    // Gets the normal Input   
    public virtual void LocalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        GetInput();
    }

    // Override to support other Inputs
    protected virtual void GetInput()
    {

    }
}
