using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerController : MonoBehaviour
{    
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float throwForce = 500f;

    private int isMovingKey = Animator.StringToHash("isMoving");
    private int moveX = Animator.StringToHash("moveX");
    private int moveY = Animator.StringToHash("moveY");
    private int isCarrying = Animator.StringToHash("isCarrying");
    private int Attack = Animator.StringToHash("Attack");
    private int Throw = Animator.StringToHash("Throw");

    private bool canMove = true;    
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

    protected Vector2 movementInput;
    protected Animator animator;
    protected PlayerInteract playerInteract;
    protected PlayerTrashCollection trashCollection;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        
        playerInteract = GetComponent<PlayerInteract>();
        trashCollection = GetComponentInChildren<PlayerTrashCollection>();
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
        }             
        else
        {            
            IsMoving = false;
        }        
    }    

    void OnFire()
    {                
        if(canMove && !playerInteract.IsHolding) animator.SetTrigger(Attack);        
    }

    void OnThrow(GameObject objectHold)
    {
        IThrowingObject throwingObject = objectHold.GetComponent<IThrowingObject>();
        if (throwingObject != null)
        {
            Vector2 playerDirection = new Vector2(animator.GetFloat(moveX), animator.GetFloat(moveY));

            throwingObject.Throw(playerDirection, throwForce);

            animator.SetTrigger(Throw);

            rb.velocity = Vector2.zero;
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
