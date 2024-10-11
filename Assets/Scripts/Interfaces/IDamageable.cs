using UnityEngine;

public interface IDamageable {

    public float Health { get; set; }

    public bool Invencible { get; set; }

    void OnHit(float damage, Vector2 knockback);
    void OnHit(float damage);
}
