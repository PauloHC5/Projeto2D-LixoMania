using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dessincronizar : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Set a random start time for the animation between 0 and 1 (which is normalized time)
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, Random.Range(0f, 1f));

        // Unpause the animation in case it was paused by setting the normalized time
        animator.speed = 1;
    }
}
