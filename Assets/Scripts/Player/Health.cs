using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Action<int> OnLifesChanged;
    public static Action<PlayerMotor> OnDeath;
    public static Action<PlayerMotor> OnRevive;

    [Header("Settings")]
    [SerializeField] private int lifes = 3;

    public int MaxLifes => maxLifes;

    public int CurrentLifes => currentLifes;

    private Animator animator;
    private int maxLifes;
    private int currentLifes;
    private bool hasTakenDamage;

    private int hurtAnimatorParameter = Animator.StringToHash("Hurting");
    private int deathAnimatorParameter = Animator.StringToHash("Dying");

    private void Awake()
    {
        maxLifes = lifes;
    }

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();

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
            animator.SetTrigger(hurtAnimatorParameter);
            currentLifes = 0;
            OnDeath?.Invoke(gameObject.GetComponent<PlayerMotor>());
            SoundManager.Instance.PlaySound(AudioLibrary.Instance.PlayerDeadClip);
        }

        UpdateLifesUI();
    }

    public void KillPlayer()
    {
        animator.SetTrigger(deathAnimatorParameter);
        currentLifes = 0;
        SoundManager.Instance.PlaySound(AudioLibrary.Instance.PlayerDeadClip);
        UpdateLifesUI();
        OnDeath?.Invoke(gameObject.GetComponent<PlayerMotor>());
    }

    public void ResetLife()
    {
        currentLifes = lifes;
        UpdateLifesUI();
    }

    public void Revive()
    {
        OnRevive?.Invoke(gameObject.GetComponent<PlayerMotor>());
    }

    private void UpdateLifesUI()
    {
        OnLifesChanged?.Invoke(currentLifes);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IDamageable>() != null && !hasTakenDamage)
        {
            hasTakenDamage = true;
            other.GetComponent<IDamageable>().Damage(gameObject.GetComponent<PlayerMotor>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            hasTakenDamage = false;
        }
    }
}
