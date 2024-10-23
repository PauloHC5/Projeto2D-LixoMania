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

    
}
