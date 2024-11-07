using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDump : MonoBehaviour, IInteractable
{
    [Range(0, 12)]
    [SerializeField] private int trashBagsAmount = 0;

    [SerializeField] private float trashSpawnTimer = 1f;    
    
    private bool isCoroutineReady = false;

    public List<Sprite> sprites;

    private SpriteRenderer spriteRenderer;    

    private bool isAccumulated = false;

    private Bounds zoneBounds; 

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();        
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

        if (isAccumulated && !isCoroutineReady) StartCoroutine(SpawnTrashInTheZoneRoutine());
    }

    public GameObject Interact()
    {
        if (trashBagsAmount > 0)
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

        GameObject trashSpawned = TrashSpawnManager.Instance.TrashToSpawn();

        if (trashSpawned != null)
        {
            Vector2 trashPos = RandomPointInBounds();
            trashSpawned.transform.position = trashPos;

            yield return new WaitForSeconds(trashSpawnTimer);
            yield return SpawnTrashInTheZoneRoutine();        
        }        

        isCoroutineReady = false;
        yield break;                
    }

    private Vector2 RandomPointInBounds()
    {
        return new Vector2(
            Random.Range(zoneBounds.min.x, zoneBounds.max.x),
            Random.Range(zoneBounds.min.y, zoneBounds.max.y)
        );
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Zone")
        {
            zoneBounds = collision.GetComponent<Collider2D>().bounds;
        }
    }

}
