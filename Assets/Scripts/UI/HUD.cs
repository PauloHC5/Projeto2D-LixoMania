using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{    
    public RectMask2D trashHUDMask;
    public Slider healthSlider;
    public Slider polutionSlider;

    [Range(0f, 200)]
    [SerializeField] private int trashHUDMaskRange;

    private GameObject player;

    private PlayerTrashCollection trashCollection;
    private PlayerHealth playerHealth;    

    void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        trashCollection = player.GetComponentInChildren<PlayerTrashCollection>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {        
        trashHUDMask.padding = new Vector4(0, trashCollection.TrashCollected * 5, 0, 0);
        healthSlider.value = playerHealth.Health;

        if(TrashSpawnManager.TrashsInTheZone >= 15 && TrashSpawnManager.TrashsInTheZone  < 50)
        {
            polutionSlider.value = 15;
        }
        else if(TrashSpawnManager.TrashsInTheZone >= 50 && TrashSpawnManager.TrashsInTheZone < 100)
        {
            polutionSlider.value = 50;
        }
        else if(TrashSpawnManager.TrashsInTheZone >= 100 && TrashSpawnManager.TrashsInTheZone < 150)
        {
            polutionSlider.value = 100;
        } 
        else if (TrashSpawnManager.TrashsInTheZone >= 150 && TrashSpawnManager.TrashsInTheZone < 200)
        {
            polutionSlider.value = 150;
        }
        else if(TrashSpawnManager.TrashsInTheZone >= 200)
        {
            polutionSlider.value = 200;
        }
        else
        {
            polutionSlider.value = 0;
        }

        //polutionSlider.value = TrashSpawnManager.TrashsInTheZone / 2;

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
}
