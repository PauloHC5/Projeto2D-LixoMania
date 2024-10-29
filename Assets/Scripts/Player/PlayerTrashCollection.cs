using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrashCollection : MonoBehaviour
{
    [SerializeField] private int trashMaxCapacity = 5;        

    [SerializeField] private int trashCollected = 0;

    private PlayerInteract playerInteract;    

    public int TrashMaxCapacity {  get { return trashMaxCapacity; } }

    public int TrashCollected { get {  return trashCollected; } }

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
            trashCollected = 0;
        }
    }
}
