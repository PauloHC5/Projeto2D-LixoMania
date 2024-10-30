using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashZoneSpawner : MonoBehaviour
{
    [SerializeField] private int trashSpawnTimer = 1;
    [SerializeField] private float spawnRadius = 1;

    private TrashBagSpawner trashBagSpawner;    

    void Awake()
    {
        trashBagSpawner = GetComponentInParent<TrashBagSpawner>();
    }

    private void Update()
    {
        if(trashBagSpawner.IsAccumulated)
        {
            InvokeRepeating(nameof(SpawnTrashInTheZone), trashSpawnTimer, trashSpawnTimer);
        }
    }

    private void SpawnTrashInTheZone()
    {
        GameObject trashToSpawn = Resources.Load<GameObject>("lataDeCoca");
        Instantiate(trashToSpawn);

        //Random.insideUnitCircle traz um ponto na tela dentro de um
        //circulo de raio 1
        //portanto a gente multiplica por um valor para que esse raio seja maior
        trashToSpawn.transform.position = Random.insideUnitCircle * spawnRadius;
    }


}
