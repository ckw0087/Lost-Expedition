using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform holder;

    // Reference of the Player owner of the Gun
    public PlayerController PlayerController { get; set; }

    private Weapon weaponEquipped;

    // Start is called before the first frame update
    private void Start()
    {
        PlayerController = GetComponent<PlayerController>();

        EquipWeapon(weapon);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Reload();
        }
    }

    // Reloads this Gun
    private void Reload()
    {
        if (weaponEquipped != null)
        {
            weaponEquipped.Reload(false);
        }
    }

    // Shoots Projectiles
    private void Shoot()
    {
        weaponEquipped.Shoot();
    }

    // Equipp a Gun
    public void EquipWeapon(Weapon newWeapon)
    {
        if (weaponEquipped == null)
        {
            weaponEquipped = Instantiate(newWeapon, holder.position, Quaternion.identity);
            weaponEquipped.WeaponController = this;
            weaponEquipped.transform.SetParent(holder);
        }
    }
}
