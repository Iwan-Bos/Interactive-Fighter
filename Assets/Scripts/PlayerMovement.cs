using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement fields
    private float horizontal;
    private bool facingRight = true;
    [SerializeField] float speed = 2f;
    [SerializeField] float jumpingPower = 4f;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    // Attack fields
    [SerializeField] GameObject basicAttackBox;

    // method for input & other updates
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        Jump();

        BasicAttack();
        
        Flip();
    }
    // method for physics updates
    private void FixedUpdate() 
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }


    // preforms a basic attack
    private void BasicAttack() 
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(basicAttack());
        }

    }
    // jumps when on the ground
    private void Jump() {
        
        if (Input.GetButton("Jump") && Grounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }
    // checks if player is on the ground
    private bool Grounded() {
       
        return Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
    }
    // flips character around based on direction held
    private void Flip() {
        // Holding right while facing left flips character around, same with holding left while facing right.
        if (horizontal < 0f && facingRight || !facingRight && horizontal > 0f)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    // basic attack timing
    private IEnumerator basicAttack() {
        
        basicAttackBox.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        basicAttackBox.SetActive(false);
    }
}
