using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 3;
    public Collider2D swordCollider;
    public float knockbackForce = 500f;

    Vector2 rightAttackOffset;

    private void Start()
    {        
        rightAttackOffset = transform.position;
    }        

    public void IsPlayerFlipped(bool flipped)
    {
        if (flipped)
        {
            transform.localPosition = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
            Debug.Log("Left Attack");
        }
        else
        {            
            transform.localPosition = rightAttackOffset;
            Debug.Log("Right Attack");
        }
    }     

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageableObject = other.GetComponent<IDamageable>();

        if(damageableObject != null)
        {
            // Calcualte the direction between character and slime                        
            Vector3 parentPosition = transform.parent.position;

            // Knockback is in direction of the swordCollider towards collider
            Vector2 direction = (Vector2)(other.transform.position - parentPosition).normalized;

            damageableObject.OnHit(damage, direction * knockbackForce);
        }               
    }
}
