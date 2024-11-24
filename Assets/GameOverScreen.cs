using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI VictoryTxt;
    [SerializeField] private TextMeshProUGUI DefeatTxt;
    
    [SerializeField] private Image restart;
    [SerializeField] private Image quit;

    public Color playSelectedColor;
    public Color exitSelectedColor;

    private Color playBaseCollor;
    private Color exitBaseCollor;

    [SerializeField]
    private int optionsIndex = 0;

    private Vector2 movementInput;

    private bool enableInput = true;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;

        playBaseCollor = restart.color;
        exitBaseCollor = quit.color;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void Update()
    {
        CheckPlayerOneInputs();

        if (Input.GetButtonDown("VERTICAL0") && enableInput)
        {
            optionsIndex -= (int)movementInput.y;

            if (optionsIndex >= 3) optionsIndex = 2;
            if (optionsIndex <= 0) optionsIndex = 1;
        }

        switch (optionsIndex)
        {
            case 1:
                restart.color = playSelectedColor;
                quit.color = exitBaseCollor;
                break;
            case 2:
                restart.color = playBaseCollor;
                quit.color = exitSelectedColor;
                break;
            default:
                restart.color = playBaseCollor;
                quit.color = exitBaseCollor;
                break;
        }

        if (Input.GetButtonDown("VERDE0") && enableInput)
        {
            if (optionsIndex == 1)
            {
                Time.timeScale = 1f;
                GameManager.Instance.UpdateGameState(GameManager.GameState.Restart);
            }

            if (optionsIndex == 2)
            {
                Time.timeScale = 1f;
                GameManager.Instance.UpdateGameState(GameManager.GameState.Exit);
            }
        }
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Victory)
        {
            VictoryTxt.enabled = true;
            DefeatTxt.enabled = false;
        }
        else if (state == GameManager.GameState.Defeat)
        {
            VictoryTxt.enabled = false;
            DefeatTxt.enabled = true;
        }
        else if (state == GameManager.GameState.Restart || state == GameManager.GameState.Exit)
        { 
            gameObject.SetActive(false);
        }
    }

    void CheckPlayerOneInputs()
    {
        // Player 1 Joystick movement input
        movementInput.y = Input.GetAxisRaw("VERTICAL0");
    }
}
