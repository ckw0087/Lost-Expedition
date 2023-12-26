using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerJetpack : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float jetpackForce = 3f;
    [SerializeField] private float jetpackFuel = 5f;

    public float JetpackFuel { get; set; }
    public float FuelLeft { get; set; }
    public float InitialFuel => jetpackFuel;

    private float fuelLeft;
    private float fuelDurationLeft;
    private bool stillHaveFuel = true;

    //private int jetpackParameter = Animator.StringToHash("Jetpack");

    protected override void InitState()
    {
        base.InitState();
        JetpackFuel = jetpackFuel;
        fuelDurationLeft = JetpackFuel;
        FuelLeft = JetpackFuel;
        UIManager.Instance.UpdateFuel(FuelLeft, JetpackFuel);
    }

    protected override void GetInput()
    {
        if (Input.GetKey(KeyCode.X))
        {
            Jetpack();
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            EndJetpack();
        }
    }

    private void Jetpack()
    {
        if (!stillHaveFuel)
        {
            return;
        }

        if (FuelLeft <= 0)
        {
            EndJetpack();
            stillHaveFuel = false;
            return;
        }

        playerController.SetVerticalForce(jetpackForce);
        playerController.Conditions.IsJetpacking = true;
        StartCoroutine(BurnFuel());
    }

    private void EndJetpack()
    {
        playerController.Conditions.IsJetpacking = false;
        StartCoroutine(Refill());
    }

    private IEnumerator BurnFuel()
    {
        float fuelConsumed = FuelLeft;
        if (fuelConsumed > 0 && playerController.Conditions.IsJetpacking && FuelLeft <= fuelConsumed)
        {
            fuelConsumed -= Time.deltaTime;
            FuelLeft = fuelConsumed;
            UIManager.Instance.UpdateFuel(FuelLeft, JetpackFuel);
            yield return null;
        }
    }

    private IEnumerator Refill()
    {
        yield return new WaitForSeconds(0.5f);
        float fuel = FuelLeft;
        while (fuel < JetpackFuel && !playerController.Conditions.IsJetpacking)
        {
            fuel += Time.deltaTime;
            FuelLeft = fuel;
            UIManager.Instance.UpdateFuel(FuelLeft, JetpackFuel);

            if (!stillHaveFuel && fuel > 0.2f)
            {
                stillHaveFuel = true;
            }

            yield return null;
        }
    }

    //public override void SetAnimation()
    //{
    //    animator.SetBool(jetpackParameter, playerController.Conditions.IsJetpacking);
    //}
}
