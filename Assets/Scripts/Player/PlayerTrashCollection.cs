using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrashCollection : MonoBehaviour
{
    [SerializeField] private int trashMaxCapacity = 10;        

    [SerializeField] private int trashCollected = 0;

    private PlayerInteract playerInteract;    

    private void Awake()
    {
        playerInteract = GetComponentInParent<PlayerInteract>();
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (trashCollected < trashMaxCapacity)
        {
            trashCollected++;
            Destroy(collision.gameObject);
            CreateTrashBag();
        }
    }   
    
    private void CreateTrashBag()
    {
        if (trashCollected >= trashMaxCapacity)
        {
            GameObject trashBag = Instantiate(Resources.Load<GameObject>("sacoDeLixo"));
            playerInteract.ObjectHold = trashBag;
        }
    }
}
