using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    protected PlayerMotor playerMotor;
    protected SpriteRenderer spriteRenderer;
    protected Collider2D myCollider2D;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider2D = GetComponent<Collider2D>();
    }

    // Contains the logic of the colletable 
    private void CollectLogic()
    {
        if (!CanBePicked())
        {
            return;
        }

        SoundManager.Instance.PlaySound(AudioLibrary.Instance.CollectibleClip);

        Collect();
        DisableCollectible();
    }

    // Override to add custom colletable behaviour
    protected virtual void Collect()
    {
        
    }

    // Disable the spriteRenderer and collider of the Collectable
    private void DisableCollectible()
    {
        myCollider2D.enabled = false;
        spriteRenderer.enabled = false;
    }

    // Returns if this colletable can pe picked, True if it is colliding with the player
    private bool CanBePicked()
    {
        return playerMotor != null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMotor>() != null)
        {
            playerMotor = other.GetComponent<PlayerMotor>();
            CollectLogic();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerMotor = null;
    }
}
