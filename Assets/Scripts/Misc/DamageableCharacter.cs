using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    [SerializeField] protected float health = 5;    

    protected int takeDamage = Animator.StringToHash("takeDamage");

    protected Animator animator;
    protected Rigidbody2D rb;
    protected Collider2D col;

    protected IEnumerator invencibleRoutine;        

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if(animator == null) animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();        
        col = GetComponent<Collider2D>();
    }           

    public virtual void OnHit(float damage, Vector2 knockback)
    {
        //Apply the force to the slime
        rb.AddForce(knockback, ForceMode2D.Impulse);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.takeDamage);        
    }

    public virtual void OnHit(float damage)
    {
        /*
         * This method will ber overwriten by the child classes
         */
    }     
}
