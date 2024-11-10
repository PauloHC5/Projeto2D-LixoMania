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
    private DamageableCharacter playerHealth;    

    void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        trashCollection = player.GetComponentInChildren<PlayerTrashCollection>();
        playerHealth = player.GetComponent<DamageableCharacter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        trashHUDMask.padding = new Vector4(0, trashCollection.TrashCollected * 5, 0, 0);
        healthSlider.value = playerHealth.Health;
        polutionSlider.value = TrashSpawnManager.TrashsInTheZone;
    }
}
