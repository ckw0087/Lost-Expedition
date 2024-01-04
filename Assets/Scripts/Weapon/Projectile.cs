using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 30f;

    // Reference of the Gun owner of this projectile
    public Weapon WeaponEquipped { get; set; }

    // Returns the shoot direction
    public Vector3 ShootDirection => shootDirection;

    // Controls the speed of this projectile
    public float Speed { get; set; }

    private Vector3 shootDirection;

    private void Awake()
    {
        Speed = speed;
    }

    private void Update()
    {
        transform.Translate(shootDirection * Speed * Time.deltaTime);
    }

    // Set the projectile direction
    public void SetDirection(Vector3 newDirection)
    {
        shootDirection = newDirection;
    }

    // Enables the projectile speed
    public void EnableProjectile()
    {
        Speed = speed;
    }

    // Disbale the projectile speed
    public void DisableProjectile()
    {
        Speed = 0f;
    }
}
