using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] private List<GameObject> trashToSpawn;
    [SerializeField] private BoxCollider2D spawnArea;
    [SerializeField] private float spawnTime = 1f;
    [SerializeField] List<Collider2D> colliders;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);        
    }

    private void Update()
    {
        transform.position = Camera.main.transform.position;
    }

    public void StartTransition()
    {
        InvokeRepeating(nameof(SpawnTrash), 0, spawnTime);
        StartCoroutine(StopTrashSpawn());
    }

    private void SpawnTrash()
    {
        Vector2 spawnPos = new Vector2(
        Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
        Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y));

        Instantiate(trashToSpawn[Random.Range(0, trashToSpawn.Count)], spawnPos, Quaternion.identity, transform);
    }

    private IEnumerator StopTrashSpawn()
    {
        yield return new WaitForSeconds(5f);
        CancelInvoke(nameof(SpawnTrash));
    }

    public IEnumerator FinishTransition()
    {
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
