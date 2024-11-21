using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerController : MonoBehaviour
{    
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] private float throwForce = 500f;
    [SerializeField] private bool canMove = true;

    private int isMovingKey = Animator.StringToHash("isMoving");
    private int moveX = Animator.StringToHash("moveX");
    private int moveY = Animator.StringToHash("moveY");
    private int isCarrying = Animator.StringToHash("isCarrying");
    private int Attack = Animator.StringToHash("Attack");
    private int Throw = Animator.StringToHash("Throw");
       
    public bool IsMoving
    {
        get { return isMoving; }
        set
        {
            isMoving = value;
            animator.SetBool(isMovingKey, isMoving);
        }
    }
    
    private Rigidbody2D rb;    
    private bool isMoving = false;    

    private Vector2 movementInput;
    private Animator animator;
    private PlayerInteract playerInteract;
    private PlayerHealth playerHealth;
    private PlayerTrashCollection trashCollection;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        
        playerInteract = GetComponent<PlayerInteract>();
        playerHealth = GetComponent<PlayerHealth>();
        trashCollection = GetComponentInChildren<PlayerTrashCollection>();
    }

    private void Start()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Start)
        {
            canMove = true;
            playerInteract.enabled = true;
            playerHealth.enabled = true;
        }
    }

    private void Update()
    {
        CheckPlayerOneInputs();                  
    }

    private void FixedUpdate()
    {        
        // If movement input is not 0, try to move
        if (movementInput != Vector2.zero && canMove)
        {
            // Acce�erate the player shile run direction is pressed
            // BUT don't allow player to run faster than the max speed in any direction                              
            rb.AddForce(movementInput * moveSpeed * 100f * Time.deltaTime, ForceMode2D.Force);            

            IsMoving = true;

            animator.SetFloat(moveX, movementInput.x);
            animator.SetFloat(moveY, movementInput.y);

            if (!AudioManager.Instance.PassosSource.isPlaying) AudioManager.Instance.PassosSource.Play();
        }             
        else
        {            
            IsMoving = false;
            if (AudioManager.Instance.PassosSource.isPlaying) AudioManager.Instance.PassosSource.Stop();
        }
        
    }    

    void OnFire()
    {
        if (canMove && !playerInteract.IsHolding)
        {
            animator.SetTrigger(Attack);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.attack);
        }
    }

    void OnThrow()
    {
        IThrowingObject throwingObject = playerInteract.ObjectHold.GetComponent<IThrowingObject>();
        if (throwingObject != null)
        {
            Vector2 playerDirection = new Vector2(animator.GetFloat(moveX), animator.GetFloat(moveY));

            playerInteract.ObjectHold = null;
            throwingObject.Throw(playerDirection, throwForce);            

            animator.SetTrigger(Throw);

            rb.velocity = Vector2.zero;

            AudioManager.Instance.PlaySFX(AudioManager.Instance.spawnTrashBag);
        }
        else Debug.Log("No Object to throw!");        
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }    

    void CheckPlayerOneInputs()
    {
        // Player 1 Joystick movement input
        movementInput.y = Input.GetAxisRaw("VERTICAL0");
        movementInput.x = Input.GetAxisRaw("HORIZONTAL0");

        // Player 1 Green Button
        if (Input.GetButtonDown("VERDE0")) OnFire();

        // Player 1 Blue Button
        if (Input.GetButtonDown("AZUL0")) playerInteract.OnInteract();

        // Player 1 Red Button
        if (Input.GetButtonDown("VERMELHO0")) Debug.Log("Player 1 Red Button pressed!");

        // Player 1 Yellow Button
        if (Input.GetButtonDown("AMARELO0")) Debug.Log("Player 1 Yellow Button pressed!");

        // Player 1 Black Button
        if (Input.GetButtonDown("PRETO0")) Debug.Log("Player 1 Black Button pressed!");

        // Player 1 White Button
        if (Input.GetButtonDown("BRANCO0"))
        {
            SceneManager.LoadScene("Praca");
        }        
    }
}
