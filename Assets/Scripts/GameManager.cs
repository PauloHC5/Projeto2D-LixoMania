using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateGameStated(GameState newState)
    {
        state = newState;

        switch (state)
        {
            case GameState.Victory: break;
            case GameState.Lose: break;
        }
    }

    public enum GameState
    {
        Victory,
        Lose
    }
}
