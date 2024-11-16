using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBag : MonoBehaviour, IThrowingObject, IInteractable
{
    private Rigidbody2D rb;   
    private Collider2D col;
    private SpriteRenderer spriteRenderer;

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

    public T Interact<T>() where T : class
    {
        if (canInteract)
        {
            col.isTrigger = true;
            rb.velocity = Vector2.zero;

            return this as T;
        }

        return null;
    }

    private void OnTriggerStay2D(Collider2D collision)
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
