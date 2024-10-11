using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;

    private Vector2 movement;

    void Update()
    {
        // Input from player (WASD or arrow keys)
        movement.x = Input.GetAxisRaw("Horizontal"); // Left/Right movement
        movement.y = Input.GetAxisRaw("Vertical");   // Up/Down movement

        // Normalizing movement for diagonal movement to prevent speed increase
        movement = movement.normalized;

        // Animation control
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        // Move the player
        transform.Translate(movement * moveSpeed * Time.fixedDeltaTime);
    }

    void UpdateAnimation()
    {
        // Check if the player is moving
        if (movement != Vector2.zero)
        {
            // Prioritize side movement animation for diagonal directions
            if (movement.x != 0)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", 0); // Zero the vertical to prioritize side animation
            }
            else
            {
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", movement.y);
            }

            // Set animator "isMoving" parameter to true
            animator.SetBool("isMoving", true);
        }
        else
        {
            // Player is idle
            animator.SetBool("isMoving", false);
        }
    }
}
