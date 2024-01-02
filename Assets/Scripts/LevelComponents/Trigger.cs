using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private GameObject trapToTrigger;

    private void Start()
    {
        if (trapToTrigger != null)
        {
            trapToTrigger.GetComponent<SpriteRenderer>().enabled = false;
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trapToTrigger.GetComponent<SpriteRenderer>().enabled = true;
            trapToTrigger.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
