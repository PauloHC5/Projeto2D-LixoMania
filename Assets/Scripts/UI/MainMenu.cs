using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image play;
    public Image exit;

    public Color playSelectedColor;
    public Color exitSelectedColor;

    private Color playBaseCollor;
    private Color exitBaseCollor;    

    [SerializeField] private int optionsIndex = 0;

    [SerializeField] private float waitToLoadTime = 2;    

    private Vector2 movementInput;

    private bool enableInput = true;

    private void Start()
    {
        playBaseCollor = play.color;
        exitBaseCollor = exit.color;

        Time.timeScale = 1f;

        Transition transition = GameObject.FindWithTag("Transition")?.GetComponent<Transition>();

        if (transition != null) StartCoroutine(transition.FinishTransition());
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
                play.color = playSelectedColor;
                exit.color = exitBaseCollor;
                break;
            case 2:
                play.color = playBaseCollor;
                exit.color = exitSelectedColor;
                break;
            default:
                play.color = playBaseCollor;
                exit.color = exitBaseCollor;
                break;
        }

        if (Input.GetButtonDown("VERDE0") && enableInput)
        {
            enableInput = false;
            Instantiate(Resources.Load("Misc/Transition")).GetComponent<Transition>().StartTransition();
            StartCoroutine(LoadSceneRoutine());            
        }
    }

    void CheckPlayerOneInputs()
    {
        // Player 1 Joystick movement input
        movementInput.y = Input.GetAxisRaw("VERTICAL0");        ;
        
    }

    private IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSeconds(waitToLoadTime);

        if(optionsIndex == 1) SceneManager.LoadScene("Praca");
        if(optionsIndex == 2) Application.Quit();
    }

}
