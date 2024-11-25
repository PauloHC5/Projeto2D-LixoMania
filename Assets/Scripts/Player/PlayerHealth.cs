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

    [Header("Events")]

    public GameEvent onPlayerHealthAtHalf;

    public float Health
    {
        set
        {
            health = value;
            if (health > 10) health = 10;
            if (health <= 0) Defeated();

            if (health <= 5) onPlayerHealthAtHalf.Raise();
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
        GameManager.Instance.deathReason = GameManager.DeathReason.PlayerDeath;
        GameManager.Instance.UpdateGameState(GameManager.GameState.Defeat);
        if (AudioManager.Instance.PassosSource && AudioManager.Instance.PassosSource.isPlaying) AudioManager.Instance.PassosSource.Stop();
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
