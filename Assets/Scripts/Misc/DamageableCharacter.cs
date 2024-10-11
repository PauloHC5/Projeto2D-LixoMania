using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{    
    [SerializeField]
    float health = 5;

    Animator animator;
    Rigidbody2D rb;

    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
            else
            {
                if(animator) animator.SetTrigger("TakeDamage");
            }
        }
        get
        {
            return health;
        }
    }    

    bool _invencible = false;

    float invencibleTimeElapsed = 0f;

    public float invencibilityTime = 0.25f;    

    public bool Invencible
    {
        get => _invencible;

        set
        {
            _invencible = value;

            if(_invencible)
            {
                invencibleTimeElapsed = 0f;
            }
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Defeated()
    {
        rb.simulated = false;
        if (animator)
        {
            animator.SetTrigger("Defeated");
        } else RemoveEnemy();
        
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        if (!_invencible)
        {
            Health -= damage;

            //Apply the force to the slime
            rb.AddForce(knockback, ForceMode2D.Impulse);

            _invencible = true;

            Debug.Log("Force: " + knockback);
        }
    }

    public void OnHit(float damage)
    {
        if (!_invencible)
        {
            Health -= damage;
            _invencible = true;
        }
    }

    private void FixedUpdate()
    {
        if (_invencible)
        {
            invencibleTimeElapsed += Time.deltaTime;

            if( invencibleTimeElapsed > invencibilityTime)
            {
                _invencible = false;
            }
        }
    }
}
