using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Action<int> OnLifesChanged;
    public static Action<PlayerMotor> OnDeath;

    [Header("Settings")]
    [SerializeField] private int lifes = 3;

    private int maxLifes;
    private int currentLifes;

    private void Awake()
    {
        maxLifes = lifes;
    }

    // Start is called before the first frame update
    private void Start()
    {
        ResetLife();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            LoseLife();
        }
    }

    public void AddLife()
    {
        currentLifes += 1;
        if (currentLifes > maxLifes)
        {
            currentLifes = maxLifes;
        }

        UpdateLifesUI();
    }

    public void LoseLife()
    {
        currentLifes -= 1;
        if (currentLifes <= 0)
        {
            currentLifes = 0;
            OnDeath?.Invoke(gameObject.GetComponent<PlayerMotor>());
        }

        UpdateLifesUI();
    }

    public void ResetLife()
    {
        currentLifes = lifes;
        UpdateLifesUI();
    }

    private void UpdateLifesUI()
    {
        OnLifesChanged?.Invoke(currentLifes);
    }

}
