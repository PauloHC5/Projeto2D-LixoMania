using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBag : MonoBehaviour, IThrowingObject, IInteractable
{
    Rigidbody2D rb;   
    Collider2D col;
    SpriteRenderer spriteRenderer;

    private bool canInteract = true;    
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }    

    public void Throw(Vector2 direction, float throwForce)
    {
        canInteract = false;
        rb.AddForce(direction * throwForce);
        rb.gravityScale = 2f;        

        Invoke(nameof(DisableGravity), 0.5f);
    }

    void DisableGravity()
    {        
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        spriteRenderer.sortingLayerName = "Default";
        canInteract = true;
    }

    public GameObject Interact()
    {
        if(canInteract)
        {
            col.isTrigger = true;

            return gameObject;
        }

        return null;        
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TrashCan" && !canInteract)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInteract playerInteract = collision.gameObject.GetComponentInParent<PlayerInteract>();

        if(playerInteract)
        {
            playerInteract.DisableHoldPos();
            col.isTrigger = false;            
        }
    }

}
