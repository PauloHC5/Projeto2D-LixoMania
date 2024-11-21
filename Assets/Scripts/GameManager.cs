using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state;    

    public PauseMenu pauseMenu;    

    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] private float waitToLoadTime = 6f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Transition transition = GameObject.FindWithTag("Transition")?.GetComponent<Transition>();

        if (transition != null) StartCoroutine(transition.FinishTransition());

        UpdateGameState(GameState.Introduction);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch(state)
        {
            case GameState.Victory: break;
            case GameState.Lose: break;

            case GameState.Restart:
                Instantiate(Resources.Load("Misc/Transition")).GetComponent<Transition>().StartTransition();
                StartCoroutine(LoadSceneRoutine("Praca"));
                pauseMenu.gameObject.SetActive(false);
                break;
            case GameState.Exit:
                Instantiate(Resources.Load("Misc/Transition")).GetComponent<Transition>().StartTransition();
                StartCoroutine(LoadSceneRoutine("Menu"));
                pauseMenu.gameObject.SetActive(false);
                break;

            case GameState.Introduction:                
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void Update() 
    {
        if (Input.GetButtonDown("MENU")) PauseGame();        
    }

    private void PauseGame()
    {
        if (pauseMenu.gameObject.activeSelf)
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }    

    private IEnumerator LoadSceneRoutine(string sceneToLoad)
    {
        yield return new WaitForSeconds(waitToLoadTime);
        SceneManager.LoadScene(sceneToLoad);
    }

    public enum GameState
    {
        Victory,
        Lose,
        Restart,     
        Exit,
        Start,
        Introduction
    }    
    
}
