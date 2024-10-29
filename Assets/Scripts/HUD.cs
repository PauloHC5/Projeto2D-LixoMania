using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI trashCollectedTxt;
    public TextMeshProUGUI trashMaxCapacityTxt;
    
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
        trashCollectedTxt.SetText(trashCollection.TrashCollected.ToString());
        trashMaxCapacityTxt.SetText(trashCollection.TrashMaxCapacity.ToString());
    }
}
