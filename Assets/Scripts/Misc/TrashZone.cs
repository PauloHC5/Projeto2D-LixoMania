using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashZone : MonoBehaviour, IInteractable
{
    [SerializeField] private int spriteListIndex = 0;

    public List<Sprite> sprites;

    SpriteRenderer spriteRenderer;

    private GameObject trashBag;

    private bool isAccumulated = false;

    public bool IsAccumulated { get { return isAccumulated; } }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {        
        InvokeRepeating(nameof(ChangeSprite), 15, 15);

        Resources.Load<GameObject>("sacoDeLixo");
    }

    // Update is called once per frame
    void Update()
    {        

        if (sprites.Count != 0 && spriteListIndex < sprites.Count && spriteListIndex >= 0)
        {
            spriteRenderer.sprite = sprites[spriteListIndex];
        }

        if (sprites.Count != 0 && spriteListIndex >= sprites.Count) isAccumulated = true;       
        else isAccumulated = false;        
    }    

    private void ChangeSprite()
    {        
        spriteListIndex++;        
    }

    public GameObject Interact()
    {
        if(spriteListIndex > 0)
        {
            spriteListIndex--;

            GameObject spawnedTrashBag = Instantiate(trashBag);

            spawnedTrashBag.GetComponent<IInteractable>().Interact();

            return spawnedTrashBag;
        }

        return null;
        
    }
}
