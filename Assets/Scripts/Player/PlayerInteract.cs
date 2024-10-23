using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{    
    [SerializeField] private Transform holdPos;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float raycastLength = 1f;        
    [SerializeField] private LayerMask raycastLayerMask;

    private GameObject objectHold;

    public GameObject ObjectHold {
        get { return objectHold; }

        set 
        {            
            IInteractable interactable = value.GetComponent<IInteractable>();

            if (interactable != null)
            {
                holdPos.gameObject.SetActive(true);
                objectHold = interactable.Interact();                                
            }
        }
    }

    private Animator animator;
    private bool isHolding;

    public bool IsHolding {
        get { return isHolding; }        
    }
        
    
    void Awake()
    {
       animator = GetComponent<Animator>();        
    }    

    // Update is called once per frame
    void Update()
    {
        isHolding = objectHold != null ? true : false;

        if (objectHold != null) objectHold.transform.position = holdPos.position;                   

        animator.SetBool("isCarrying", objectHold != null ? true : false);
        
    }

    public void OnInteract()
    {
        if (!isHolding)
        {
            RaycastHit2D hitResult = ProjectRaycast();            
            
            if (!hitResult.collider.isTrigger) ObjectHold = hitResult.collider.gameObject;
        }
        else
        {
            SendMessage("OnThrow", objectHold);            
            objectHold = null;
        }          
    }
    
    private RaycastHit2D ProjectRaycast()
    {
        Vector2 playerDirection = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, playerDirection, raycastLength, raycastLayerMask);

        Debug.DrawRay(raycastOrigin.position, playerDirection * raycastLength, Color.red, 5f);

        return hit;
    }

    public void DisableHoldPos()
    {
        holdPos.gameObject.SetActive(false);
    }
}
