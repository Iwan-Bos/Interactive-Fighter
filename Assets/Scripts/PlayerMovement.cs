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
    public EnvironmentTrigger environmentTrigger;



    // # MAIN #
    // UPDATE, MAIN LOOP
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Jump();

        // BasicAttack();

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
        // check if it is not an Enemy
        if (other.gameObject.layer != 8 /* 8 = enemy layer*/)
        {
            // check the type & do what the trigger should do
            switch (other.gameObject.GetComponent<EnvironmentTrigger>().triggerType)
            {
                // Darkness trigger
                case 0:
                    environmentTrigger.AddDarkness();
                break;

                // Coldness trigger
                case 1:
                    environmentTrigger.AddColdness();
                break;

                // If you get here something's gone wrong
                default:
                    Debug.Log("Something's gone wrong");
                break;
            }
        }
        else // anything other than an EnviromentTrigger is an enemy (for now)
        {
            // reduce health by 1
            health -= other.GetComponent<Enemy>().contactDamage;

            // update health bar value
            healthbar.UpdateHealth(health);

            // death check
            if (health <= 0)
            {
                EndGame();
            }

            // TODO: 
            // get launched in the opposite direction
        }
    }

    public void takeDamage(int amount)
    {
        //reduce health
        health -= amount;

        //check if alive
        if (health <= 0)
        {
            EndGame();
        }

        //other stuff
    }

    // ENDGAME, CALLED WHEN PLAYER DIES
    private void EndGame() 
    {
        // exit play mode
        UnityEditor.EditorApplication.isPlaying = false;

        // TODO:
        // switch to endscreen
    }




    // # METHODS #
    // preforms a basic attack
    public void BasicAttack()
    {
        Debug.Log("attack!");
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

    // move to the right and left from teensy
    public void move(string direction)
    {
        if (direction == "left")
        {
            // Debug.Log(this.gameObject.transform.position.x);
            this.gameObject.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            if (facingRight)
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = false;
            }
        }
        else if (direction == "right")
        {
            // Debug.Log(this.gameObject.transform.position.x);
            this.gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            if (!facingRight)
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = true;
            }
        }
    }

    // jumps when on the ground
    public void Jump()
    {
        if (Grounded())
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