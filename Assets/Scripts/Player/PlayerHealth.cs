using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : DamageableCharacter
{
    [SerializeField] protected float invencibilityTime = 0.25f;    
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private LayerMask layerToIgnoreWhenInvencible;

    protected bool invencible = false;

    private SpriteRenderer spriteRenderer;    

    public float Health
    {
        set
        {
            health = value;
            if (health > 10) health = 10;
            if (health <= 0) Defeated();
        }
        get { return health; }
    }

    public bool Invencible
    {
        get => invencible;

        set => invencible = value;       
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Defeated()
    {
        rb.simulated = false;
        if (deathVFX)
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

    public override void OnHit(float damage, Vector2 knockback)
    {
        base.OnHit(damage, knockback);

        if (!invencible)
        {
            Health -= damage;
            Invencible = true;
            StartCoroutine(InvencibleRoutine());                        
        }        

        StartCoroutine(BlinkPlayer());
    }    

    private IEnumerator BlinkPlayer()
    {
        while (invencible)
        {
            //spriteRenderer.enabled = false;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.25f);
            yield return new WaitForSeconds(invencibilityTime / 8);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            yield return new WaitForSeconds(invencibilityTime / 8);
        }        

        yield break;        
    }

    private IEnumerator InvencibleRoutine()
    {
        LayerMask defaultLayer = col.excludeLayers;
        col.excludeLayers = layerToIgnoreWhenInvencible;
        yield return new WaitForSeconds(invencibilityTime);
        col.excludeLayers = defaultLayer;
        invencible = false;
    }
}
