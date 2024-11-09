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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            animator.SetBool(isClosed, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool(isClosed, true);
        }
    }
}
