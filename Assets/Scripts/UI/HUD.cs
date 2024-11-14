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

        polutionSlider.maxValue = trashManager.SceneMaxTrash;

        float onequarter = trashManager.SceneMaxTrash / 4f;
        float half = trashManager.SceneMaxTrash / 2f;
        float threeQuarter = trashManager.SceneMaxTrash / 1.5f;

        if (trashManager.TrashsInTheZone >= onequarter && trashManager.TrashsInTheZone < half)
        {
            polutionSlider.value = onequarter;
        }
        else if (trashManager.TrashsInTheZone >= half && trashManager.TrashsInTheZone < threeQuarter)
        {
            polutionSlider.value = half;
        }
        else if (trashManager.TrashsInTheZone >= threeQuarter && trashManager.TrashsInTheZone < trashManager.SceneMaxTrash)
        {
            polutionSlider.value = threeQuarter;
        }
        else if(trashManager.TrashsInTheZone >= trashManager.SceneMaxTrash)
        {
            polutionSlider.value = trashManager.SceneMaxTrash;
        }
        else
        {
            polutionSlider.value = 0;
        }
    }    
}
