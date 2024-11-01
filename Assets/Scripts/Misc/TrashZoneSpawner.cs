using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashZoneSpawner : MonoBehaviour
{
    [SerializeField] private float trashSpawnTimer = 1f;
    [SerializeField] private int zoneMaxTrash = 50;

    private int trashsInTheZoneCount = 0;
    private bool isCoroutineReady = false;    

    private TrashZone trashZone;
    private CapsuleCollider2D capsuleCollider;

    void Awake()
    {
        trashZone = GetComponentInParent<TrashZone>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }        

    private void Update()
    {
        if (trashZone.IsAccumulated && !isCoroutineReady && trashsInTheZoneCount < zoneMaxTrash) StartCoroutine(SpawnTrashInTheZoneRoutine());        
    }

    private IEnumerator SpawnTrashInTheZoneRoutine()
    {        
        isCoroutineReady = true;

        GameObject trashToSpawn = Resources.Load<GameObject>("lataDeCoca");
        Vector2 trashPosition = RandomPointInBounds(capsuleCollider.bounds);
        Instantiate(trashToSpawn, trashPosition, Quaternion.identity);

        yield return new WaitForSeconds(trashSpawnTimer);

        if (trashsInTheZoneCount < zoneMaxTrash)
        {
            yield return SpawnTrashInTheZoneRoutine();
        }
        else
        {
            isCoroutineReady = false;
        }
            
    }    

    private Vector2 RandomPointInBounds(Bounds bounds)
    {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trashsInTheZoneCount++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        trashsInTheZoneCount--;
    }

}
