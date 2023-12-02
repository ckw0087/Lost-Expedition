using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private PlayerStates[] playerStates;

    // Start is called before the first frame update
    private void Start()
    {
        playerStates = GetComponents<PlayerStates>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerStates.Length != 0)
        {
            foreach (PlayerStates state in playerStates)
            {
                state.LocalInput();
                state.ExecuteState();
                state.SetAnimation();
            }
        }
    }

    public void SpawnPlayer(Transform newPosition)
    {
        transform.position = newPosition.position;
    }
}
