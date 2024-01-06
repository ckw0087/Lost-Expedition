using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlockTrigger : MonoBehaviour
{
    [SerializeField] private GameObject fallingBlockTrap;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D myRigidbody;

    private void Start()
    {
        spriteRenderer = fallingBlockTrap.GetComponent<SpriteRenderer>();
        boxCollider = fallingBlockTrap.GetComponent<BoxCollider2D>();
        myRigidbody = fallingBlockTrap.GetComponent<Rigidbody2D>();

        if (fallingBlockTrap != null)
        {
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.enabled = true;
            boxCollider.enabled = true;
            myRigidbody.isKinematic = false;

            Destroy(transform.parent.gameObject, 3f);
        }
    }
}
