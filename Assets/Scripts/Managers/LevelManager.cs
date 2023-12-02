using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static Action<PlayerMotor> OnPlayerSpawn;

    [Header("Settings")]
    [SerializeField] private Transform levelStartPoint;
    [SerializeField] private GameObject playerPrefab;

    private PlayerMotor currentPlayer;

    private void Awake()
    {
        SpawnPlayer(playerPrefab);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RevivePlayer();
        }
    }

    // Spawns our player in the spawnPoint   
    private void SpawnPlayer(GameObject player)
    {
        if (player != null)
        {
            currentPlayer = Instantiate(player, levelStartPoint.position, Quaternion.identity).GetComponent<PlayerMotor>();
            currentPlayer.GetComponent<Health>().ResetLife();

            // Call Event
            OnPlayerSpawn?.Invoke(currentPlayer);
        }
    }

    // Revives our player
    private void RevivePlayer()
    {
        if (currentPlayer != null)
        {
            currentPlayer.gameObject.SetActive(true);
            currentPlayer.SpawnPlayer(levelStartPoint);
            currentPlayer.GetComponent<Health>().ResetLife();
            currentPlayer.GetComponent<Health>().Revive();
        }
    }

    private void PlayerDeath(PlayerMotor player)
    {
        currentPlayer.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Health.OnDeath += PlayerDeath;
    }

    private void OnDisable()
    {
        Health.OnDeath -= PlayerDeath;
    }
}
