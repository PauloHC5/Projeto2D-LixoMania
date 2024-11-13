using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : DamageableCharacter
{
    [SerializeField] private GameObject deathVFX;

    private NPC npc;

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
        npc = GetComponent<NPC>();
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

        Health -= damage;
        npc.StunNPC();
        if (animator) animator.SetTrigger(takeDamage);             
    }
}
