using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public static Action OnLevelCompleted;

    [SerializeField] private GameObject levelCompletedPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Level Complete!");
            OnLevelCompleted?.Invoke();
            levelCompletedPanel.SetActive(true);          
        }
    }
}
