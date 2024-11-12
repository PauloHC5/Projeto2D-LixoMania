using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{    
    [SerializeField] private float health = 5;
    [SerializeField] private float invencibilityTime = 0.25f;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private LayerMask layerToIgnoreWhenInvencible;

    private int takeDamage = Animator.StringToHash("takeDamage");

    private Animator animator;
    protected Rigidbody2D rb;
    private Collider2D col;

    private IEnumerator invencibleRoutine;

    public float Health
    {
        set
        {
            health = value;
            if(health > 10) health = 10;
            if (health <= 0) Defeated();                        
        }
        get { return health; }
    }    

    private bool invencible = false;    

    public bool Invencible
    {
        get => invencible;

        set
        {
            invencible = value;            
        }
    }

    private NPC npc;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if(animator == null) animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        npc = GetComponent<NPC>();
        col = GetComponent<Collider2D>();
    }    

    public void Defeated()
    {
        rb.simulated = false;       
        if(deathVFX)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            RemoveCharacter();
        }        
        else RemoveCharacter();
        
    }

    private void RemoveCharacter()
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

            Invencible = true;
            if(invencibleRoutine == null) invencibleRoutine = InvencibleRoutine();
            StartCoroutine(invencibleRoutine);            

            if (animator) animator.SetTrigger(takeDamage);
            if (npc != null) npc.StunNPC();

            AudioManager.Instance.PlaySFX(AudioManager.Instance.takeDamage);
        }
    }

    public void OnHit(float damage)
    {
        if (!invencible)
        {
            Health -= damage;
            Invencible = true;
        }
    }

    private IEnumerator InvencibleRoutine()
    {        
        col.excludeLayers |= (1 << LayerMask.NameToLayer("Enemies"));
        yield return new WaitForSeconds(invencibilityTime);
        col.excludeLayers &= ~(1 << LayerMask.NameToLayer("Enemies"));
        invencible = false;
    }    
}
