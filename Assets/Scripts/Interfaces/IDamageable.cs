using UnityEngine;

public interface IDamageable {
    
    public bool Invencible { get; set; }

    void OnHit(float damage, Vector2 knockback);
    void OnHit(float damage);
}
