using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{    
    public RectMask2D trashHUDMask;
    public Slider healthSlider;
    public Slider polutionSlider;
    public GameObject botao_azul;
    public GameObject botao_verde;
    public GameObject relogio;

    [Range(0f, 200)]
    [SerializeField] private int trashHUDMaskRange;

    [SerializeField] private GameObject player;

    [Header("Events")]

    public GameEvent polutionAtHalf;

    private PlayerTrashCollection trashCollection;
    private PlayerHealth playerHealth;    

    private TrashSpawnManager trashManager;

    void Awake()
    {        
        trashCollection = player.GetComponentInChildren<PlayerTrashCollection>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        trashManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<TrashSpawnManager>();

        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Start)
        {
            trashHUDMask.gameObject.SetActive(true);
            healthSlider.gameObject.SetActive(true);
            polutionSlider.gameObject.SetActive(true);
            botao_azul.gameObject.SetActive(true);
            botao_verde.gameObject.SetActive(true);
            relogio.gameObject.SetActive(true);
        }
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
            polutionAtHalf.Raise();
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
