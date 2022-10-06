using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class PlayerMovement : MonoBehaviour
{
    // # FIELDS #
    // Movement
    private float horizontal;
    private bool facingRight = true;
    [SerializeField] float speed = 2f;
    [SerializeField] float jumpingPower = 4f;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
 

    // Stats
    [SerializeField] int health;    

    // Scripts
    public Collide collide;
    public Healthbar healthbar;



    // # MAIN #
    // UPDATE, MAIN LOOP
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        Jump();

        BasicAttack();

        Flip();
    }

    // FIXEDUPDATE, RIGIDBODY LOOP
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    // ONTRIGGERENTER, CALLED WHEN ENTERING ANOTHER COLLIDER
    private void OnTriggerEnter(Collider other) 
    {
        // reduce health by 1
        health--;

        // update health bar value
        healthbar.UpdateHealth(health);

        // death check
        if (health <= 0)
        {
            EndGame();
        }

        // get launched in the opposite direction

        // log health
        Debug.Log(this.health);
    }

    // ENDGAME, CALLED WHEN PLAYER DIES
    private void EndGame() 
    {
        // exit play mode
        UnityEditor.EditorApplication.isPlaying = false;
    }



    // # METHODS #
    // preforms a basic attack
    private void BasicAttack()
    {
        // on click
        if (Input.GetMouseButtonDown(0))
        {
            // check what is in the hitbox
           Collider[] hitColliders = collide.OverlapBox();

            // reset i
            int i = 0;
        
            // check when there is a new collider coming into contact with the box
            while (i < hitColliders.Length)
            {
                // log gameObject of hit collider
                Debug.Log(hitColliders[i].gameObject);

                // call Hit() of the hit collider to change hit enemies' values
                hitColliders[i].gameObject.GetComponent<Enemy>().Hit();

                // increment i
                i++;
            }
        }
    }
    // jumps when on the ground
    private void Jump()
    {
        if (Input.GetButton("Jump") && Grounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }
    // checks if player is on the ground
    private bool Grounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
    }
    // flips character around based on direction held
    private void Flip()
    {
        // Holding right while facing left flips character around, same with holding left while facing right.
        if (horizontal < 0f && facingRight || !facingRight && horizontal > 0f)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}