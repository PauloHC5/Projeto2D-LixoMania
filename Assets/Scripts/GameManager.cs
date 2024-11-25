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

    public GameOverScreen gameOverScreen;

    public PlayerController playerController;

    public TrashSpawnManager trashSpawnManager;

    public static event Action<GameState> OnGameStateChanged;  

    public DeathReason deathReason = DeathReason.None;
    
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
            case GameState.Victory:
                gameOverScreen.gameObject.SetActive(true);
                AudioManager.Instance.StopMusic();
                AudioManager.Instance.PlaySFX(AudioManager.Instance.victoryMusic);
                break;
            case GameState.Defeat:
                gameOverScreen.gameObject.SetActive(true);
                AudioManager.Instance.StopMusic();
                AudioManager.Instance.PlaySFX(AudioManager.Instance.defeatMusic);
                break;

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

        if (canCountTimeElapsed && (state != GameState.Victory && state != GameState.Defeat)) timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeToWin * 60 && state == GameState.Start)
        {            
            UpdateGameState(GameState.Victory);            
        }

        if (trashSpawnManager.TrashsInTheScene >= trashSpawnManager.SceneMaxTrash)
        {
            UpdateGameState(GameState.Defeat);
            deathReason = DeathReason.TrashAtMax;
        }
    }

    private void PauseGame()
    {

        if(state != GameState.Introduction && state != GameState.Victory && state != GameState.Defeat)
        {
            if (!pauseMenu.gameObject.activeSelf)
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
    }    

    private IEnumerator LoadSceneRoutine(string sceneToLoad)
    {
        yield return new WaitForSeconds(waitToLoadTime);
        SceneManager.LoadScene(sceneToLoad);
    }    

    public enum GameState
    {
        Victory,
        Defeat,
        Restart,     
        Exit,
        Start,
        Introduction
    }    

    public enum DeathReason
    {
        None,
        TrashAtMax,
        PlayerDeath
    }
    
}
