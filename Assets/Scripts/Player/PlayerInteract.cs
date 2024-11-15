using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{    
    [SerializeField] private Transform holdPos;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float raycastLength = 1f;        
    [SerializeField] private LayerMask raycastLayerMask;

    private TrashBag objectHold;    

    public TrashBag ObjectHold {
        get { return objectHold; }

        set 
        {                                                    
            objectHold = value;                                            
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
        holdPos.gameObject.SetActive(objectHold ? true : false);

        if (objectHold != null) objectHold.transform.position = holdPos.position;                   

        animator.SetBool("isCarrying", objectHold != null ? true : false);
        
    }

    public void OnInteract()
    {

        RaycastHit2D hitResult = ProjectRaycast();        

        if(hitResult.collider && !isHolding)
        {
            IInteractable interactable = hitResult.collider?.GetComponent<IInteractable>();

            if (interactable.Interact<TrashBag>())
            {
                ObjectHold = interactable.Interact<TrashBag>();
            }
        } else if(isHolding)
        {
            SendMessage("OnThrow", GetComponent<PlayerController>());
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
