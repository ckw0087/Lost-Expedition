using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFuel : Collectible
{
    [Header("Settings")]
    [SerializeField] private float extraFuel = 10f;
    [SerializeField] private float extraFuelTimer = 3f;

    private PlayerJetpack jetpack;

    protected override void Collect()
    {
        ApplyFuel();
    }

    // Apply that bonus
    private void ApplyFuel()
    {
        jetpack = playerMotor.GetComponent<PlayerJetpack>();
        StartCoroutine(IEExtraFuel());
    }

    // Adds fuel 
    private IEnumerator IEExtraFuel()
    {
        jetpack.JetpackFuel = extraFuel;
        jetpack.FuelLeft = extraFuel;
        UIManager.Instance.UpdateFuel(extraFuel, extraFuel);
        yield return new WaitForSeconds(extraFuelTimer);
        jetpack.JetpackFuel = jetpack.InitialFuel;
        jetpack.FuelLeft = jetpack.InitialFuel;
        UIManager.Instance.UpdateFuel(jetpack.FuelLeft, jetpack.JetpackFuel);
    }
}
