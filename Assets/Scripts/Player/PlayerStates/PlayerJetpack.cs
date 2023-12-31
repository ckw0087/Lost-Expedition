using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerJetpack : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float jetpackForce = 3f;
    [SerializeField] private float jetpackFuel = 5f;

    private float fuelLeft;
    private float fuelDurationLeft;
    private bool stillHaveFuel = true;

    //private int jetpackParameter = Animator.StringToHash("Jetpack");

    protected override void InitState()
    {
        base.InitState();
        fuelDurationLeft = jetpackFuel;
        fuelLeft = jetpackFuel;
        UIManager.Instance.UpdateFuel(fuelLeft, jetpackFuel);
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

        if (fuelLeft <= 0)
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
        float fuelConsumed = fuelLeft;
        if (fuelConsumed > 0 && playerController.Conditions.IsJetpacking && fuelLeft <= fuelConsumed)
        {
            fuelConsumed -= Time.deltaTime;
            fuelLeft = fuelConsumed;
            UIManager.Instance.UpdateFuel(fuelLeft, jetpackFuel);
            yield return null;
        }
    }

    private IEnumerator Refill()
    {
        yield return new WaitForSeconds(0.5f);
        float fuel = fuelLeft;
        while (fuel < jetpackFuel && !playerController.Conditions.IsJetpacking)
        {
            fuel += Time.deltaTime;
            fuelLeft = fuel;
            UIManager.Instance.UpdateFuel(fuelLeft, jetpackFuel);

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
