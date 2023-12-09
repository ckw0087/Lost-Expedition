using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public static Action<float> OnJump;

    [Header("Settings")]
    [SerializeField] private float jumpHeight = 5f;

    private Animator animator;
    private int jumperParameter = Animator.StringToHash("Jumper");

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerJump>() != null)
        {
            OnJump?.Invoke(jumpHeight);
            animator.SetTrigger(jumperParameter);
        }
    }
}
