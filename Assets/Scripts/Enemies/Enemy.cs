using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 1f;
    public float knockbackForce = 200f;
    public float moveSpeed = 500f;
    public DetectionZone detectionZone;    

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {        
        if (detectionZone.detectedObjs.Count > 0)
        {
            Collider2D detectedObject = detectionZone.detectedObjs[0];

            // Move towards detected object
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed, ForceMode2D.Force);
        }
    }

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
