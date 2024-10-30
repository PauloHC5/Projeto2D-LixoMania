using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashZoneSpawner : MonoBehaviour
{    
    [SerializeField] private int trashSpawnTimer = 1;
    [SerializeField] private float spawnRadius = 1.0f;    

    private TrashZone trashZone;    

    void Awake()
    {
        trashZone = GetComponentInParent<TrashZone>();        
    }

    private void Update()
    {
        if(trashZone.IsAccumulated)
        {
            InvokeRepeating(nameof(SpawnTrashInTheZone), trashSpawnTimer, trashSpawnTimer);
        }
    }

    private void SpawnTrashInTheZone()
    {
        GameObject trashToSpawn = Resources.Load<GameObject>("lataDeCoca");        

        Vector2 trashPosition = new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle * spawnRadius;        

        Instantiate(trashToSpawn, trashPosition, Quaternion.identity);          
    }

}
