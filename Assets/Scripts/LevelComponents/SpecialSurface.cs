using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSurface : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float friction = 0.1f;

    [Header("Movement")]
    [SerializeField] private float horizontalMovement = 4f;

    public float Friction => friction;

    private PlayerController playerController;

    private void Update()
    {
        if (playerController == null)
        {
            return;
        }

        playerController.AddHorizontalMovement(horizontalMovement);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            playerController = other.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerController = null;
    }
}
