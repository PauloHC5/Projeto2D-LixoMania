using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashDump : MonoBehaviour, IInteractable
{
    [Range(0, 6)]
    [SerializeField] private int trashBagsAmount = 0;

    [SerializeField] private float timeToAddTragBag = 15;

    [SerializeField] private float trashSpawnTimer = 1f;

    [SerializeField] private GameObject trashAreaSpawn;

    private bool isCoroutineReady = false;

    public List<Sprite> trasgBagsSprites;
    public List<GameObject> trashBagsShadows;

    [Header("Events")]

    public GameEvent onTrashDumpAccumulated;

    private SpriteRenderer spriteRenderer;    

    private bool isAccumulated = false;

    public bool IsAccumulated {  get { return isAccumulated; } }    

    private CapsuleCollider2D capsuleCollider;

    private IEnumerator restartInvokeChangeSpriteRoutine;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();        
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Start()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;        
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Start)
        {
           InvokeRepeating(nameof(ChangeSprite), timeToAddTragBag, timeToAddTragBag);
        }
        else if(state == GameManager.GameState.Victory || state == GameManager.GameState.Defeat)
        {
            CancelInvoke(nameof(ChangeSprite));
            StopAllCoroutines();
        }
    }

    private void ChangeSprite()
    {
        if (trashBagsAmount < trasgBagsSprites.Count)
        {
            ++trashBagsAmount;
            if (trashBagsAmount == trasgBagsSprites.Count) onTrashDumpAccumulated.Raise();
        }
            
    }

    // Update is called once per frame
    void Update()
    {

        if (trasgBagsSprites.Count != 0 && trashBagsAmount < trasgBagsSprites.Count && trashBagsAmount >= 0)
        {
            spriteRenderer.sprite = trasgBagsSprites[trashBagsAmount];            
        }

        if(trashBagsAmount <= 0) capsuleCollider.enabled = false;
        else capsuleCollider.enabled = true;        

        AlternateTrashBagsShadowsWithTheAmount();

        if (trasgBagsSprites.Count != 0 && trashBagsAmount >= trasgBagsSprites.Count) isAccumulated = true;
        else isAccumulated = false;

        if (isAccumulated && !isCoroutineReady) StartCoroutine(SpawnTrashInTheZoneRoutine());

        if (!isAccumulated) StopCoroutine(SpawnTrashInTheZoneRoutine());
        
    }    

    public T Interact<T>() where T : class
    {
        if (trashBagsAmount > 0)
        {            
            if (restartInvokeChangeSpriteRoutine == null)
            {
                restartInvokeChangeSpriteRoutine = RestartInvokeChangeSpriteRoutine();
                StartCoroutine(restartInvokeChangeSpriteRoutine);
            }

            trashBagsAmount--;

            TrashBag spawnedTrashBag = Instantiate(Resources.Load<TrashBag>("Lixos/sacoDeLixo"));

            spawnedTrashBag.Interact<TrashBag>();

            return spawnedTrashBag as T;
        }

        return null;
    }

    private IEnumerator RestartInvokeChangeSpriteRoutine()
    {
        CancelInvoke(nameof(ChangeSprite));
        yield return new WaitForSeconds(60);
        InvokeRepeating(nameof(ChangeSprite), 0, timeToAddTragBag);        
        restartInvokeChangeSpriteRoutine = null;

    }

    private IEnumerator SpawnTrashInTheZoneRoutine()
    {
        if(!isAccumulated)
        {
            isAccumulated = false;
            yield break;
        }

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
            Random.Range(trashAreaSpawn.GetComponent<BoxCollider2D>().bounds.min.x, trashAreaSpawn.GetComponent<BoxCollider2D>().bounds.max.x),
            Random.Range(trashAreaSpawn.GetComponent<BoxCollider2D>().bounds.min.y, trashAreaSpawn.GetComponent<BoxCollider2D>().bounds.max.y)
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
}
