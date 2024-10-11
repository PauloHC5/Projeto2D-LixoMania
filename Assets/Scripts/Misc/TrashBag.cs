using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBag : MonoBehaviour, IThrowingObject, IInteractable
{
    Rigidbody2D rb;   
    Collider2D col;
    SpriteRenderer spriteRenderer;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }    

    public void Throw(Vector2 direction, float throwForce)
    {        
        rb.AddForce(direction * throwForce);
        rb.gravityScale = 2f;

        Invoke(nameof(DisableGravity), 0.5f);
    }

    void DisableGravity()
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        spriteRenderer.sortingLayerName = "Default";        
    }

    public GameObject Interact()
    {
        col.isTrigger = true;

        return gameObject;
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TrashCan")
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
