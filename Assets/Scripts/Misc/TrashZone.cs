using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashZone : MonoBehaviour, IInteractable
{
    [Range(0, 12)]
    [SerializeField] private int trashBagsAmount = 0;

    [SerializeField] private float trashSpawnTimer = 1f;
    [SerializeField] private int zoneMaxTrash = 50;

    private int trashsInTheZoneCount = 0;
    private bool isCoroutineReady = false;

    public List<Sprite> sprites;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private bool isAccumulated = false;    

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponentInChildren<BoxCollider2D>();
    }

    void Start()
    {        
        InvokeRepeating(nameof(ChangeSprite), 15, 15);        
    }

    private void ChangeSprite()
    {
        trashBagsAmount++;
    }

    // Update is called once per frame
    void Update()
    {        

        if (sprites.Count != 0 && trashBagsAmount < sprites.Count && trashBagsAmount >= 0)
        {
            spriteRenderer.sprite = sprites[trashBagsAmount];
        }

        if (sprites.Count != 0 && trashBagsAmount >= sprites.Count) isAccumulated = true;       
        else isAccumulated = false;

        if (isAccumulated && 
            !isCoroutineReady && 
            trashsInTheZoneCount < zoneMaxTrash) StartCoroutine(SpawnTrashInTheZoneRoutine());
    }    

    public GameObject Interact()
    {
        if(trashBagsAmount > 0)
        {
            trashBagsAmount--;

            GameObject spawnedTrashBag = Instantiate(Resources.Load<GameObject>("Lixos/sacoDeLixo"));

            spawnedTrashBag.GetComponent<IInteractable>().Interact();

            return spawnedTrashBag;
        }

        return null;
        
    }

    private IEnumerator SpawnTrashInTheZoneRoutine()
    {
        isCoroutineReady = true;

        string trashToSpawnName = "Lixos/" + ChooseRandomTrash();

        GameObject trashToSpawn = Resources.Load<GameObject>(trashToSpawnName);

        if (trashToSpawn != null)
        {
            Vector2 trashPosition = RandomPointInBounds(boxCollider.bounds);
            Instantiate(trashToSpawn, trashPosition, Quaternion.identity);
        }
        else Debug.Log("N achei lixo: " + trashToSpawnName);

        yield return new WaitForSeconds(trashSpawnTimer);

        if (trashsInTheZoneCount < zoneMaxTrash) yield return SpawnTrashInTheZoneRoutine();        
        else isCoroutineReady = false;       
    }

    private Vector2 RandomPointInBounds(Bounds bounds)
    {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trashsInTheZoneCount++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        trashsInTheZoneCount--;
    }
}
