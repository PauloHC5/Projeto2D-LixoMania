using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{    
    public RectMask2D trashHUDMask;

    [Range(0f, 200)]
    [SerializeField] private int trashHUDMaskRange;

    private GameObject player;

    private PlayerTrashCollection trashCollection;

    void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        trashCollection = player.GetComponentInChildren<PlayerTrashCollection>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        trashHUDMask.padding = new Vector4(0, trashCollection.TrashCollected * 20, 0, 0);        
    }
}
