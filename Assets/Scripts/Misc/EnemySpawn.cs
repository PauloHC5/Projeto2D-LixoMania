using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private float spawnTime = 3;
    [SerializeField] private int maxEnemiesSpawn = 10;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    private bool spawnActive = false;
    private int enemiesSpawnCount = 0;

    private TrashDump trashDump;

    private void Awake()
    {
        trashDump = GetComponentInParent<TrashDump>();
    }

    // Update is called once per frame
    void Update()
    {
        if(trashDump.IsAccumulated && !spawnActive)
        {
            InvokeRepeating(nameof(SpawnEnemies), spawnTime, spawnTime);
            spawnActive = true;
            enemiesSpawnCount = 0;
        }

        if (!trashDump.IsAccumulated)
        {
            CancelInvoke(nameof(SpawnEnemies));
            spawnActive = false;
        }
    }

    private void SpawnEnemies()
    {
        if (enemiesSpawnCount >= maxEnemiesSpawn)
        {
            CancelInvoke(nameof(SpawnEnemies));            
        }
        
        Instantiate(enemies[Random.Range(0, 2)], transform.position, Quaternion.identity);                
        enemiesSpawnCount++;        
    }
}
