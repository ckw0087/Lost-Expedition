using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHealth : Collectible
{
    protected override void Collect()
    {
        AddLife();
    }

    // Adds life
    private void AddLife()
    {
        if (playerMotor.GetComponent<Health>() == null)
        {
            return;
        }

        Health playerHealth = playerMotor.GetComponent<Health>();
        if (playerHealth.CurrentLifes < playerHealth.MaxLifes)
        {
            playerHealth.AddLife();
        }
    }
}
