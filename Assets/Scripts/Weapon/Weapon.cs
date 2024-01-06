using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;

    [Header("Weapon Settings")]
    [SerializeField] private float msBetweenShots = 100;

    [Header("Ammo")]
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private bool autoReload = true;
    [SerializeField] private float reloadTime = 3f;

    // Reference of the GunController 
    public WeaponController WeaponController { get; set; }

    private ObjectPooler pooler;
    private float nextShotTime;
    private float reloadTimer;
    private bool isReloading;
    private int projectilesRemaining;


    private void Start()
    {
        pooler = GetComponent<ObjectPooler>();
        projectilesRemaining = magazineSize;
    }

    private void Update()
    {
        if (autoReload)
        {
            Reload(true);
        }
    }

    // Fires a projectile from the firePoint
    private void FireProjectile()
    {
        // Get Object from pool
        GameObject newProjectile = pooler.GetObjectFromPool();
        newProjectile.transform.position = firePoint.position;
        newProjectile.SetActive(true);

        // Get projectile
        Projectile projectile = newProjectile.GetComponent<Projectile>();
        projectile.WeaponEquipped = this;
        projectile.SetDirection(WeaponController.PlayerController.FacingRight ? Vector3.right : Vector3.left);
        projectile.EnableProjectile();
    }

    // Shoots our weapon
    public void Shoot()
    {
        if (Time.time > nextShotTime && !isReloading && projectilesRemaining > 0)
        {
            nextShotTime = Time.time + msBetweenShots / 1000f;

            FireProjectile();
            projectilesRemaining--;

            SoundManager.Instance.PlaySound(AudioLibrary.Instance.ProjectileClip);
        }
    }

    // Reloads this gun
    public void Reload(bool autoReload)
    {
        if (projectilesRemaining > 0 && projectilesRemaining <= magazineSize && !isReloading && !autoReload)
        {
            StartCoroutine(IEWaitForReload());
        }

        if (projectilesRemaining <= 0 && !isReloading)
        {
            StartCoroutine(IEWaitForReload());
        }
    }

    // Reload coroutine
    private IEnumerator IEWaitForReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        projectilesRemaining = magazineSize;
        isReloading = false;
    }
}
