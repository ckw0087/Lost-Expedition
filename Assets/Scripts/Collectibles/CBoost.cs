using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBoost : Collectible
{
    [Header("Settings")]
    [SerializeField] private float boostSpeed = 30f;
    [SerializeField] private float boostTime = 3f;

    private PlayerMovement playerMovement;

    protected override void Collect()
    {
        ApplyMovement();
    }

    // Apply that movement bonus
    private void ApplyMovement()
    {
        playerMovement = playerMotor.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            StartCoroutine(IEBoost());
        }
    }

    // Add boost to our player movement
    private IEnumerator IEBoost()
    {
        playerMovement.Speed = boostSpeed;
        yield return new WaitForSeconds(boostTime);
        playerMovement.Speed = playerMovement.InitialSpeed;
    }
}
