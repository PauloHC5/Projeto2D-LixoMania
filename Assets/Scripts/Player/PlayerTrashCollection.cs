using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrashCollection : MonoBehaviour
{
    [SerializeField] private int trashMaxCapacity = 10;

    private int trashCollected;

    public int TrashCollected
    {
        get { return trashCollected; }
        set 
        {
            if (trashCollected <= trashMaxCapacity) trashCollected = value; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
