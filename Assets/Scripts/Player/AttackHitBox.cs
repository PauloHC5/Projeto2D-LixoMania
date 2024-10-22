using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    private PlayerTrashCollection trashCollection;

    private void Awake()
    {
        trashCollection = GetComponentInParent<PlayerTrashCollection>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trashCollection.TrashCollected += 1;
        Destroy(collision.gameObject);
    }
}
