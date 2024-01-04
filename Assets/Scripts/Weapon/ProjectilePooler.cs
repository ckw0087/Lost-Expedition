using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    // Event raised when colliding
    public static Action<Collider2D> OnProjectileCollision;

    [Header("Settings")]
    [SerializeField] private LayerMask collideWith;

    private Projectile projectile;

    private void Start()
    {
        projectile = GetComponent<Projectile>();
    }

    private void Update()
    {
        CheckCollisions();
    }

    // Checks for collisions in order to call some logic
    private void CheckCollisions()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, projectile.ShootDirection,
            projectile.Speed * Time.deltaTime + 0.2f, collideWith);

        if (hit)
        {
            OnProjectileCollision?.Invoke(hit.collider);
            projectile.DisableProjectile();
            gameObject.SetActive(false);
        }
    }
}
