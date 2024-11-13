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

    private TrashSpawnManager trashManager;

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
        trashManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().trashManager;
    }

    // Update is called once per frame
    void Update()
    {        
        trashHUDMask.padding = new Vector4(0, trashCollection.TrashCollected * 5, 0, 0);
        healthSlider.value = playerHealth.Health;

        if(trashManager.TrashsInTheZone >= 15 && trashManager.TrashsInTheZone  < 50)
        {
            polutionSlider.value = 15;
        }
        else if(trashManager.TrashsInTheZone >= 50 && trashManager.TrashsInTheZone < 100)
        {
            polutionSlider.value = 50;
        }
        else if(trashManager.TrashsInTheZone >= 100 && trashManager.TrashsInTheZone < 150)
        {
            polutionSlider.value = 100;
        } 
        else if (trashManager.TrashsInTheZone >= 150 && trashManager.TrashsInTheZone < 200)
        {
            polutionSlider.value = 150;
        }
        else if(trashManager.TrashsInTheZone >= 200)
        {
            polutionSlider.value = 200;
        }
        else
        {
            polutionSlider.value = 0;
        }            
    }    
}
