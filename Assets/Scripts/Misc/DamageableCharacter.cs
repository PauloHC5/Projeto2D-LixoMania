using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{    
    [SerializeField] private float health = 5;
    [SerializeField] private float invencibilityTime = 0.25f;
    [SerializeField] private GameObject deathVFX;

    private int takeDamage = Animator.StringToHash("takeDamage");

    private Animator animator;
    private Rigidbody2D rb;

    public float Health
    {
        set
        {
            health = value;
            if (health <= 0) Defeated();            
            else
            {                
                if(animator) animator.SetTrigger(takeDamage);
                if(npc != null) npc.StunNPC();
            }
        }
        get { return health; }
    }    

    private bool invencible = false;

    private float invencibleTimeElapsed = 0f;

    public bool Invencible
    {
        get => invencible;

        set
        {
            invencible = value;

            if(invencible)
            {
                invencibleTimeElapsed = 0f;
            }
        }
    }

    private NPC npc;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if(animator == null) animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        npc = GetComponent<NPC>();
    }    

    public void Defeated()
    {
        rb.simulated = false;       
        if(deathVFX)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            RemoveEnemy();
        }        
        else RemoveEnemy();
        
    }

    private void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        if (!invencible)
        {
            Health -= damage;

            //Apply the force to the slime
            rb.AddForce(knockback, ForceMode2D.Impulse);

            invencible = true;

            AudioManager.Instance.PlaySFX(AudioManager.Instance.takeDamage);
        }
    }

    public void OnHit(float damage)
    {
        if (!invencible)
        {
            Health -= damage;
            invencible = true;
        }
    }

    private void FixedUpdate()
    {
        if (invencible)
        {
            invencibleTimeElapsed += Time.deltaTime;

            if( invencibleTimeElapsed > invencibilityTime)
            {
                invencible = false;
            }
        }
    }
}
