using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AriaController : MonoBehaviour
{
    
    [Header("Movement Settings")]
    public float moveSpeed = 5f;       
    public float jumpForce = 7f;        

    [Header("Ground Check")]
    public Transform groundCheck;      
    public float groundCheckRadius = 0.1f; 
    public LayerMask groundLayer;       

   
    private Rigidbody2D rb;
    private bool isGrounded;           
    private float moveInput;            

    public enum PlayerState { Idle, Running, Jumping }
    public PlayerState currentState;  

    void Start()
    {
    
        rb = GetComponent<Rigidbody2D>();
        
        currentState = PlayerState.Idle;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); 

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateState();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(0.5f, 1f, 1f); 
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-0.5f, 1f, 1f); 
        }
    }

    void UpdateState()
    {
        if (!isGrounded)
        {
            currentState = PlayerState.Jumping;
        }
        else if (Mathf.Abs(moveInput) > 0.1f)
        {
            currentState = PlayerState.Running;
        }
        else
        {
            currentState = PlayerState.Idle;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}