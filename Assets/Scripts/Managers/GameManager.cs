using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameStates
    {
        GameStart,
        LevelCompleted
    }

    // The Current State of our Game
    public GameStates GameState { get; set; }

    protected override void Awake()
    {
        base.Awake();        
    }

    private void Start()
    {
        GameState = GameStates.GameStart;
    }

    // Response to the levelcompleted event
    private void LevelCompleted()
    {
        GameState = GameStates.LevelCompleted;
    }

    private void OnEnable()
    {
        LevelEnd.OnLevelCompleted += LevelCompleted;
    }

    private void OnDisable()
    {
        LevelEnd.OnLevelCompleted -= LevelCompleted;
    }
}
