using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Action Melee Target", fileName = "ActionMeleeTarget")]
public class ActionMeleeTarget : AIAction
{
    public float attackRate = 1f; // Attacks per second
    private float lastAttackTime;

    public override void Act(StateController controller)
    {
        if (Time.time >= lastAttackTime + (1f / attackRate))
        {
            PerformMeleeAttack(controller);
            lastAttackTime = Time.time;
        }
    }

    private void PerformMeleeAttack(StateController controller)
    {
        float distanceToPlayer = Vector3.Distance(controller.transform.position, controller.Target.transform.position);
        float attackRange = 1.5f; // Example attack range

        if (distanceToPlayer <= attackRange)
        {
            // Player is in range, apply damage
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.GetComponent<Health>().LoseLife();
            }      
        }
    }
}
