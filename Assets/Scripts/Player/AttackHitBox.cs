using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        IDamageable damageableObject = collision.collider.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            // Calcualte the direction between character and slime                        
            Vector3 parentPosition = transform.parent.position;

            // Knockback is in direction of the swordCollider towards collider
            Vector2 direction = (Vector2)(collision.transform.position - parentPosition).normalized;

            damageableObject.OnHit(1f, direction * 2f);
        }
        else Destroy(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageableObject = collision.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            // Calcualte the direction between character and slime                        
            Vector3 parentPosition = transform.parent.position;

            // Knockback is in direction of the swordCollider towards collider
            Vector2 direction = (Vector2)(collision.transform.position - parentPosition).normalized;

            damageableObject.OnHit(1f, direction * 2f);
        }
        else Destroy(collision.gameObject);
    }


}
