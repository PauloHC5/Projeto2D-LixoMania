using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCTrashSpawner : MonoBehaviour
{
    // List to hold all trash items in the scene
    private List<GameObject> trashItems = new List<GameObject>();

    // Time interval range for spawning trash (min and max delay between spawns)
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 5f;

    // Spawn position offset (if you want to adjust the spawn height, etc.)
    public Vector3 spawnOffset = new Vector3(0, -0.5f, 0); // Adjust based on your game

    // Start is called before the first frame update
    void Start()
    {
        // Find all objects tagged as "trash" in the scene and add them to the list
        GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("trash");
        foreach (GameObject trash in trashObjects)
        {            
            trashItems.Add(trash); 
        }

        // Start the trash spawn routine
        StartCoroutine(SpawnTrashRoutine());
    }

    // Coroutine to handle random trash spawning
    IEnumerator SpawnTrashRoutine()
    {
        while (true)
        {
            // Wait for a random time between minSpawnTime and maxSpawnTime
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            // Spawn a random trash item
            SpawnTrash();
        }
    }

    // Method to spawn a random trash item
    void SpawnTrash()
    {
        if (trashItems.Count == 0)
        {
            Debug.LogWarning("No trash items found in the scene!");
            return;
        }

        // Choose a random trash item from the list
        int randomIndex = Random.Range(0, trashItems.Count);        
        
        GameObject trashToSpawn = (GameObject)trashItems[randomIndex];

        // Get the position and rotation of the selected trash item
        Vector3 spawnPosition = transform.position + spawnOffset;
        Quaternion spawnRotation = trashToSpawn.transform.rotation;

        // Instantiate the trash item with its original rotation
        Instantiate(trashToSpawn, spawnPosition, spawnRotation);                
    }
}