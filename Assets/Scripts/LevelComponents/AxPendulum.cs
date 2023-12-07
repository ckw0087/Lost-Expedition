using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxPendulum : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    [SerializeField] private bool instantKill;

    public void Damage(PlayerMotor player)
    {
        if (player != null)
        {
            if (instantKill)
            {
                player.GetComponent<Health>().KillPlayer();
            }
            else
            {
                player.GetComponent<Health>().LoseLife();
            }
        }
    }
}
