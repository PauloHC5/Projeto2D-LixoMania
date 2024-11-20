using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuTrashSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> trashToSpawn;
    [SerializeField] private GameObject someParent;
    [SerializeField] private float spawnTime = 1f;     
    

    private BoxCollider2D boxCol;

    private void Awake()
    {
        boxCol = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnTrash), 0, spawnTime);
        StartCoroutine(StopTrashSpawn());
    }
    

    private void SpawnTrash()
    {
        Vector2 spawnPos = new Vector2(
            Random.Range(boxCol.bounds.min.x, boxCol.bounds.max.x),
            Random.Range(boxCol.bounds.min.y, boxCol.bounds.max.y));

        Instantiate(trashToSpawn[Random.Range(0, trashToSpawn.Count)], spawnPos, Quaternion.identity, someParent.transform);
    }

    private IEnumerator StopTrashSpawn()
    {
        yield return new WaitForSeconds(5f);
        CancelInvoke(nameof(SpawnTrash));
    }
}
