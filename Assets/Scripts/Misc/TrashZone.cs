using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrashZone : MonoBehaviour
{
    public UnityEvent IncrementTrash;
    public UnityEvent DecrementTrash;

    private void Start() 
    {
        IncrementTrash.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<TrashSpawnManager>().IncrementTrashSpawned);
        DecrementTrash.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<TrashSpawnManager>().DecrementTrashSpawned);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.gameObject.tag == "trash")
        {
            IncrementTrash.Invoke();
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {        
        if (collision.gameObject.tag == "trash")
        {
            DecrementTrash.Invoke();
        }
    }    
}
