using UnityEngine;

public interface IDamageable {
        
    void OnHit(float damage, Vector2 knockback);
    void OnHit(float damage);
}
