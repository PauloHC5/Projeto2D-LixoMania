using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 10f;
    [SerializeField] private bool targeatable = true;
    
    public float Health { get=> health; set { health = value > 0 ? value : health; } }
    public bool Invencible { get => targeatable; set => targeatable = value; }

    public void OnHit(float damage, Vector2 knockback)
    {
        throw new System.NotImplementedException();
    }

    public void OnHit(float damage)
    {
        throw new System.NotImplementedException();
    }
    
}
