using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Image title;
    public Image restart;
    public Image exit;

    public Color playSelectedColor;
    public Color exitSelectedColor;

    private Color playBaseCollor;
    private Color exitBaseCollor;

    [SerializeField]
    private int optionsIndex = 0;    

    private Vector2 movementInput;

    private bool enableInput = true;

    public static PauseMenu Instance;     

    private void Start()
    {
        playBaseCollor = restart.color;
        exitBaseCollor = exit.color;

        Instance = this;
        gameObject.SetActive(false);
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
                exit.color = exitBaseCollor;
                break;
            case 2:
                restart.color = playBaseCollor;
                exit.color = exitSelectedColor;
                break;
            default:
                restart.color = playBaseCollor;
                exit.color = exitBaseCollor;
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

    void CheckPlayerOneInputs()
    {
        // Player 1 Joystick movement input
        movementInput.y = Input.GetAxisRaw("VERTICAL0");
    }
        
}
