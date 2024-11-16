using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrashCollection : MonoBehaviour
{
    [SerializeField] private int trashMaxCapacity = 5;        

    [SerializeField] private int trashCollected = 0;

    [SerializeField] private int damage = 1;

    [SerializeField] float knockbackForce = 5f;

    private PlayerInteract playerInteract;    

    public int TrashMaxCapacity {  get { return trashMaxCapacity; } }

    public int TrashCollected { get {  return trashCollected; } }

    private void Awake()
    {
        playerInteract = GetComponentInParent<PlayerInteract>();
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if(damageable != null)
        {
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector2 direction = (Vector2) (collision.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;

            damageable.OnHit(damage, knockback);

            return;
        }

        if (trashCollected < trashMaxCapacity)
        {
            trashCollected++;
            Destroy(collision.gameObject);
            CreateTrashBag();
        }


    }   
    
    private void CreateTrashBag()
    {
        if (trashCollected >= trashMaxCapacity)
        {
            TrashBag trashBag = Instantiate(Resources.Load<TrashBag>("Lixos/sacoDeLixo"));            
            playerInteract.ObjectHold = trashBag.Interact<TrashBag>();
            trashCollected = 0;

            AudioManager.Instance.PlaySFX(AudioManager.Instance.spawnTrashBag);
        }
    }
}
