using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    public float damage = 1f;
    public float knockbackForce = 200f;    
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D other = collision.collider;

        IDamageable damageable = collision.collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // Calcualte the direction between character and slime                        
            Vector3 parentPosition = transform.position;

            // Knockback is in direction of the swordCollider towards collider
            Vector2 direction = (Vector2)(other.transform.position - parentPosition).normalized;

            damageable.OnHit(damage, direction * knockbackForce);
                     
        }
    }
}
