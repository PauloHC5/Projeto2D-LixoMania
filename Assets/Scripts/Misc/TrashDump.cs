using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDump : MonoBehaviour, IInteractable
{
    [Range(0, 6)]
    [SerializeField] private int trashBagsAmount = 0;

    [SerializeField] private float timeToAddTragBag = 15;

    [SerializeField] private float trashSpawnTimer = 1f;    
    
    private bool isCoroutineReady = false;

    public List<Sprite> trasgBagsSprites;
    public List<GameObject> trashBagsShadows;

    private SpriteRenderer spriteRenderer;    

    private bool isAccumulated = false;

    public bool IsAccumulated {  get { return isAccumulated; } }

    private Bounds zoneBounds; 

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();        
    }

    void Start()
    {
        InvokeRepeating(nameof(ChangeSprite), timeToAddTragBag, timeToAddTragBag);
    }

    private void ChangeSprite()
    {
        trashBagsAmount++;
    }

    // Update is called once per frame
    void Update()
    {

        if (trasgBagsSprites.Count != 0 && trashBagsAmount < trasgBagsSprites.Count && trashBagsAmount >= 0)
        {
            spriteRenderer.sprite = trasgBagsSprites[trashBagsAmount];            
        }

        AlternateTrashBagsShadowsWithTheAmount();

        if (trasgBagsSprites.Count != 0 && trashBagsAmount >= trasgBagsSprites.Count) isAccumulated = true;
        else isAccumulated = false;

        if (isAccumulated && !isCoroutineReady) StartCoroutine(SpawnTrashInTheZoneRoutine());
    }

    public GameObject Interact()
    {
        if (trashBagsAmount > 0)
        {
            StartCoroutine(RestartInvokeChangeSprite());

            trashBagsAmount--;

            GameObject spawnedTrashBag = Instantiate(Resources.Load<GameObject>("Lixos/sacoDeLixo"));

            spawnedTrashBag.GetComponent<IInteractable>().Interact();            

            return spawnedTrashBag;
        }

        return null;

    }

    private IEnumerator RestartInvokeChangeSprite()
    {
        CancelInvoke(nameof(ChangeSprite));
        yield return new WaitForSeconds(60);
        InvokeRepeating(nameof(ChangeSprite), timeToAddTragBag, timeToAddTragBag);

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
    
    private void AlternateTrashBagsShadowsWithTheAmount()
    {
        switch (trashBagsAmount)
        {
            case 0:
                foreach (var item in trashBagsShadows)
                {
                    item.SetActive(false);
                }
                break;

            case 1:
                trashBagsShadows[0].SetActive(true);
                trashBagsShadows[1].SetActive(false);
                trashBagsShadows[2].SetActive(false);
                trashBagsShadows[3].SetActive(false);
                trashBagsShadows[4].SetActive(false);
                trashBagsShadows[5].SetActive(false);
                break;

            case 2:

                trashBagsShadows[0].SetActive(true);
                trashBagsShadows[1].SetActive(true);
                trashBagsShadows[2].SetActive(false);
                trashBagsShadows[3].SetActive(false);
                trashBagsShadows[4].SetActive(false);
                trashBagsShadows[5].SetActive(false);
                break;

            case 3:
                trashBagsShadows[0].SetActive(true);
                trashBagsShadows[1].SetActive(true);
                trashBagsShadows[2].SetActive(true);
                trashBagsShadows[3].SetActive(false);
                trashBagsShadows[4].SetActive(false);
                trashBagsShadows[5].SetActive(false);
                break;

            case 4:
                trashBagsShadows[0].SetActive(true);
                trashBagsShadows[1].SetActive(true);
                trashBagsShadows[2].SetActive(true);
                trashBagsShadows[3].SetActive(true);
                trashBagsShadows[4].SetActive(false);
                trashBagsShadows[5].SetActive(false);
                break;

            case 5:
                trashBagsShadows[0].SetActive(true);
                trashBagsShadows[1].SetActive(true);
                trashBagsShadows[2].SetActive(true);
                trashBagsShadows[3].SetActive(true);
                trashBagsShadows[4].SetActive(true);
                trashBagsShadows[5].SetActive(false);
                break;

            case 6:
                trashBagsShadows[0].SetActive(true);
                trashBagsShadows[1].SetActive(true);
                trashBagsShadows[2].SetActive(true);
                trashBagsShadows[3].SetActive(true);
                trashBagsShadows[4].SetActive(true);
                trashBagsShadows[5].SetActive(true);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Zone")
        {
            zoneBounds = collision.GetComponent<Collider2D>().bounds;
        }
    }

}
