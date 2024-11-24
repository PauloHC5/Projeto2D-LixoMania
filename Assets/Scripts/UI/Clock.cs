using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private GameObject clockPointer;   
    
    private GameManager gameManager;

    private float firstHour, secondHour, thirdHour, fourthHour, fifthHour, sixthHour, seventhHour, eighthHour, ninthHour;

    private void Start()
    {
        gameManager = GameManager.Instance;

        firstHour = gameManager.TimeToWin / 9;
        secondHour = firstHour * 2;
        thirdHour = firstHour * 3; 
        fourthHour = firstHour * 4;
        fifthHour = firstHour * 5; 
        sixthHour = firstHour * 6; 
        seventhHour = firstHour * 7;
        eighthHour = firstHour * 8; 
        ninthHour = firstHour * 9; 
    }

    private void Update()
    {


        if (gameManager.TimeElapsed >= firstHour && gameManager.TimeElapsed < secondHour) 
        {
            clockPointer.transform.rotation = Quaternion.Euler(clockPointer.transform.rotation.x, clockPointer.transform.rotation.y, 90f);
        }
        else if (gameManager.TimeElapsed >= secondHour && gameManager.TimeElapsed < thirdHour) 
        {
            clockPointer.transform.rotation = Quaternion.Euler(clockPointer.transform.rotation.x, clockPointer.transform.rotation.y, 60f);
        }
        else if (gameManager.TimeElapsed >= thirdHour && gameManager.TimeElapsed < fourthHour) 
        {
            clockPointer.transform.rotation = Quaternion.Euler(clockPointer.transform.rotation.x, clockPointer.transform.rotation.y, 30f);
        }
        else if (gameManager.TimeElapsed >= fourthHour && gameManager.TimeElapsed < fifthHour) 
        {
            clockPointer.transform.rotation = Quaternion.Euler(clockPointer.transform.rotation.x, clockPointer.transform.rotation.y, 0f);
        }
        else if (gameManager.TimeElapsed >= fifthHour && gameManager.TimeElapsed < sixthHour) 
        {
            clockPointer.transform.rotation = Quaternion.Euler(clockPointer.transform.rotation.x, clockPointer.transform.rotation.y, -30f);
        }
        else if (gameManager.TimeElapsed >= sixthHour && gameManager.TimeElapsed < seventhHour) 
        {
            clockPointer.transform.rotation = Quaternion.Euler(clockPointer.transform.rotation.x, clockPointer.transform.rotation.y, -60f);
        }
        else if (gameManager.TimeElapsed >= seventhHour && gameManager.TimeElapsed < eighthHour)
        {
            clockPointer.transform.rotation = Quaternion.Euler(clockPointer.transform.rotation.x, clockPointer.transform.rotation.y, -90f);
        }
        else if (gameManager.TimeElapsed >= eighthHour && gameManager.TimeElapsed < ninthHour) 
        {
            clockPointer.transform.rotation = Quaternion.Euler(clockPointer.transform.rotation.x, clockPointer.transform.rotation.y, -120f);
        }        
        else if(gameManager.TimeElapsed >= ninthHour) 
        {
            clockPointer.transform.rotation = Quaternion.Euler(clockPointer.transform.rotation.x, clockPointer.transform.rotation.y, -150f);            
            enabled = false;
        }
    }
}
