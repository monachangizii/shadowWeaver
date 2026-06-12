using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionSystem : MonoBehaviour
{
    [Header("Interaction Settings")]
    public Transform interactPoint;      
    public float interactRadius = 0.8f;  
    public LayerMask boxLayer;           
    public float pushSpeed = 3f;          

    private Rigidbody2D detectedBox;  
    private bool isPushing = false;      

    void Update()
    {
        detectedBox = null;
        Collider2D hit = Physics2D.OverlapCircle(interactPoint.position, interactRadius, boxLayer);
        
        if (hit != null)
        {
            detectedBox = hit.GetComponent<Rigidbody2D>();
        }

        isPushing = Input.GetKey(KeyCode.E);
    }

    void FixedUpdate()
    {
        if (detectedBox != null && isPushing)
        {
            float direction = transform.localScale.x > 0 ? 1 : -1;
            
            detectedBox.velocity = new Vector2(direction * pushSpeed, detectedBox.velocity.y);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (interactPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(interactPoint.position, interactRadius);
        }
    }
}