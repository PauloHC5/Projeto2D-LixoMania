using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseTrashCan : MonoBehaviour
{
    private Animator animator;

    private int isClosed = Animator.StringToHash("isClosed");

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }    

    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (collision.GetComponent<PlayerInteract>().IsHolding)
        {
            animator.SetBool(isClosed, false);
        }
        else animator.SetBool(isClosed, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool(isClosed, true);
        }
    }
}
