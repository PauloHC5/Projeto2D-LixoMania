using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float waitToLoadTime = 6f;
    [SerializeField] private float timeToWin = 9f;

    [SerializeField] private float timeElapsed = 0f;
    private bool canCountTimeElapsed = false;

    [Header("References")]
    public static GameManager Instance;

    public GameState state;    

    public PauseMenu pauseMenu;    

    public PlayerController playerController;

    public static event Action<GameState> OnGameStateChanged;  
    
    public float TimeElapsed { get => timeElapsed; }

    public float TimeToWin { get => timeToWin * 60; }

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
            
            case GameState.Start:
                playerController.gameObject.SetActive(true);
                canCountTimeElapsed = true;
                break;
                
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void Update() 
    {
        if (Input.GetButtonDown("MENU")) PauseGame();

        if (canCountTimeElapsed && state != GameState.Victory) timeElapsed += Time.deltaTime;
    }

    private void PauseGame()
    {
        if (!pauseMenu.gameObject.activeSelf && state != GameState.Introduction)
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0f;            
        }
        else
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1f;
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
