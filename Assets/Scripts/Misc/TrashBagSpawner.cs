using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBagSpawner : MonoBehaviour, IInteractable
{
    public GameObject trashBag;

    [SerializeField]
    int spriteListIndex = 0;

    public List<Sprite> sprites;

    SpriteRenderer spriteRenderer;    
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {        
        InvokeRepeating(nameof(ChangeSprite), 15, 15);
    }

    // Update is called once per frame
    void Update()
    {
        if (sprites.Count != 0 && spriteListIndex < sprites.Count && spriteListIndex >= 0)
        {
            spriteRenderer.sprite = sprites[spriteListIndex];
        }
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
