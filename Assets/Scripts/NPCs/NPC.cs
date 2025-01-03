using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Movement speed
    [SerializeField] protected float speed = 2f;
    [SerializeField] private float stunTime = 2f;

    // Boundaries for the movement area
    public float minX = -14.5f;
    public float maxX = 14.5f;
    public float minY = -25f;
    public float maxY = 25f;

    // Time to change direction
    [SerializeField] private float changeDirectionTime = 2f;
    private float timer = 0f;

    public float minTimeDirection = 2.0f;

    // Movement direction
    private Vector2 direction;

    // Store the original scale
    private Vector3 originalScale;

    // Reference to the shadow child
    public GameObject shadowChild;

    // Variable to track the shadow rotation state
    [SerializeField] private bool isNPCRotated = false;
    [SerializeField] private NPCTrashSpawner trashSpawner;

    private bool canMove = true;    

    private Rigidbody2D rb;
    private IEnumerator npcStunRoutine;

    public float StunTime { get { return stunTime; } }
    public bool IsNPCRotated {  get { return isNPCRotated; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();     
        trashSpawner = GetComponent<NPCTrashSpawner>();
    }        

    void Start()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;

        // Store the original scale of the object
        originalScale = transform.localScale;        

        // Start with a random direction
        ChooseRandomDirection();        
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Start)
        {
            if (trashSpawner != null) trashSpawner.enabled = true;
        }
        else if(state == GameManager.GameState.Victory || state == GameManager.GameState.Defeat)
        {
            if (trashSpawner != null) trashSpawner.enabled = false;
            canMove = false;
        }
    }

    void Update()
    {                        
        // Check boundaries
        CheckBounds();

        // Timer to change direction
        timer += Time.deltaTime;
        if (timer >= changeDirectionTime && canMove)
        {
            ChooseRandomDirection();
            timer = 0f;
        }

        // Flip the object based on the direction
        FlipObject();
    }

    private void FixedUpdate()
    {
        // Move the object                
        if(canMove)
        {
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            rb.MovePosition(currentPosition + direction * speed * Time.deltaTime);
        }        
    }

    public void StunNPC()
    {
        canMove = false;
        if(npcStunRoutine != null) StopCoroutine(npcStunRoutine);
        npcStunRoutine = NPCStunRoutine();
        StartCoroutine(npcStunRoutine);                                
    }

    private IEnumerator NPCStunRoutine()
    {
        yield return new WaitForSeconds(stunTime);
        canMove = true;
    }
    void ChooseRandomDirection()
    {
        // Choose a random direction (8 directions: up, down, left, right, and diagonals)
        int randomDir = Random.Range(0, 8);
        switch (randomDir)
        {
            case 0: direction = Vector2.right; break;                   // Right
            case 1: direction = Vector2.left; break;                    // Left
            case 2: direction = Vector2.up; break;                      // Up
            case 3: direction = Vector2.down; break;                    // Down
            case 4: direction = new Vector2(1, 1).normalized; break;    // Up-Right
            case 5: direction = new Vector2(-1, 1).normalized; break;   // Up-Left
            case 6: direction = new Vector2(1, -1).normalized; break;   // Down-Right
            case 7: direction = new Vector2(-1, -1).normalized; break;  // Down-Left
        }
    }

    void CheckBounds()
    {
        // If the object is outside the horizontal boundaries, change direction
        if (transform.position.x < minX)
        {
            transform.position = new Vector2(minX, transform.position.y);
            ChooseRandomDirection();
        }
        else if (transform.position.x > maxX)
        {
            transform.position = new Vector2(maxX, transform.position.y);
            ChooseRandomDirection();
        }

        // If the object is outside the vertical boundaries, change direction
        if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, minY);
            ChooseRandomDirection();
        }
        else if (transform.position.y > maxY)
        {
            transform.position = new Vector2(transform.position.x, maxY);
            ChooseRandomDirection();
        }
    }

    void FlipObject()
    {
        // Flip the NPC based on the direction
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            isNPCRotated = false;
            RotateShadowChild(false); // Rotate back if moving right
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            isNPCRotated = true;
            RotateShadowChild(true); // Rotate by 90 degrees if moving left
        }
    }

    void RotateShadowChild(bool flip)
    {
        if (shadowChild != null)
        {
            if (flip && !isNPCRotated)
            {
                shadowChild.transform.Rotate(0, 0, 90);  // Rotate by 90 degrees on the Z-axis                
            }
            else if (!flip && isNPCRotated)
            {
                shadowChild.transform.Rotate(0, 0, -90); // Rotate back by -90 degrees on the Z-axis
                
            }
        }
    }

    // Handle collision with obstacles or NPCs
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Change direction when colliding with an obstacle
        if (timer >= minTimeDirection)
        {
            direction = (collision.contacts[0].normal).normalized;
        }
        else
        {
            timer = 0f;
            ChooseRandomDirection();
        }
    }
}
