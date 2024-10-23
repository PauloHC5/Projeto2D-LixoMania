using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrashCollection : MonoBehaviour
{
    [SerializeField] private int trashMaxCapacity = 10;        

    [SerializeField] private int trashCollected = 0;

    [SerializeField] private bool canCollectTrash = true;

    public int TrashCollected
    {
        get { return trashCollected; }
        set 
        {
            if (trashCollected <= trashMaxCapacity) trashCollected = value; 
        }
    }

    public bool CanCollectTrash {
        get { return canCollectTrash; }
    }

    private void Update() {
        canCollectTrash = trashCollected < trashMaxCapacity ? true : false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {                
        TrashCollected++;
        Destroy(collision.gameObject);                                
    }    
}
