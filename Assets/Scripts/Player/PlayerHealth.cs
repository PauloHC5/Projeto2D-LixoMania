using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : DamageableCharacter
{
    [SerializeField] private float health = 5;    
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private LayerMask layerToIgnoreWhenInvencible;

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
        if (!invencible) Health -= damage;

        base.OnHit(damage, knockback);

        StartCoroutine(BlinkPlayer());
    }

    public override void OnHit(float damage)
    {
        if (!invencible)
        {
            Health -= damage;
            Invencible = true;            
        }
    }

    private IEnumerator BlinkPlayer()
    {
        while (invencible)
        {
            Debug.Log("To aqui");
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 70f);
            yield return new WaitForSeconds(1.0f);
            //spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 255f);
        }

        yield break;
    }
}
