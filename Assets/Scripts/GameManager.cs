using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state;

    public TrashSpawnManager trashManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Update() 
    {
        if (Input.GetButtonDown("MENU")) PauseGame();        
    }

    private void PauseGame()
    {
        if (PauseMenu.Instance.gameObject.activeSelf)
        {
            PauseMenu.Instance.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            PauseMenu.Instance.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
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
