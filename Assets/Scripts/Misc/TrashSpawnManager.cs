using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawnManager : MonoBehaviour
{
    private static TrashSpawnManager instance;

    public TrashSpawnManager Instance { get { return instance; } }

    [SerializeField] private int zoneMaxTrash = 50;

    private int trashsInTheZoneCount = 0;

    public GameObject TrashToSpawn()
    {

        if (trashsInTheZoneCount < zoneMaxTrash)
        {
            string trashToSpawnName = "Lixos/" + ChooseRandomTrash();

            GameObject trashToSpawn = Resources.Load<GameObject>(trashToSpawnName);

            if (trashToSpawn != null)
            {                
                return Instantiate(trashToSpawn);
            }
            else Debug.Log("N achei lixo: " + trashToSpawnName);
        }
        
        return null;
    }    

    private string ChooseRandomTrash()
    {
        // Choose a random direction (8 directions: up, down, left, right, and diagonals)
        int randomDir = Random.Range(0, 4);
        switch (randomDir)
        {
            case 0: return "lataDeCoca";
            case 1: return "banana";
            case 2: return "garrafaDeVidro";
            case 3: return "lataDePepsi";
            case 4: return "sacolaDePlastico";
        }

        return "";
    }    
}
