using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlockTrigger : MonoBehaviour
{
    [SerializeField] private GameObject fallingBlockTrap;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D myRigidbody;

    private int triggerAnimatorParameter = Animator.StringToHash("Trigger");

    private void Start()
    {
        spriteRenderer = fallingBlockTrap.GetComponent<SpriteRenderer>();
        animator = fallingBlockTrap.GetComponent<Animator>();
        myRigidbody = fallingBlockTrap.GetComponent<Rigidbody2D>();

        if (fallingBlockTrap != null)
        {
            spriteRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger(triggerAnimatorParameter);
            spriteRenderer.enabled = true;
            myRigidbody.isKinematic = false;

            Destroy(transform.parent.gameObject, 5f);
        }
    }
}
