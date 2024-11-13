using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    [SerializeField] protected float invencibilityTime = 0.25f;
    [SerializeField] protected bool invencible = false;

    protected int takeDamage = Animator.StringToHash("takeDamage");

    protected Animator animator;
    protected Rigidbody2D rb;
    protected Collider2D col;

    protected IEnumerator invencibleRoutine;

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

    public virtual void OnHit(float damage, Vector2 knockback)
    {
        if (!invencible)
        {            
            //Apply the force to the slime
            rb.AddForce(knockback, ForceMode2D.Impulse);

            Invencible = true;            
            StartCoroutine(InvencibleRoutine());            

            if (animator) animator.SetTrigger(takeDamage);
            if (npc != null) npc.StunNPC();

            AudioManager.Instance.PlaySFX(AudioManager.Instance.takeDamage);
        }
    }

    public virtual void OnHit(float damage)
    {
        
    }

    private IEnumerator InvencibleRoutine()
    {        
        col.excludeLayers |= (1 << LayerMask.NameToLayer("Enemies"));
        yield return new WaitForSeconds(invencibilityTime);
        col.excludeLayers &= ~(1 << LayerMask.NameToLayer("Enemies"));
        invencible = false;
    }    
}
