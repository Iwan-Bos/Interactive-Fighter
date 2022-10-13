using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int health;
    [SerializeField] ParticleSystem HitParticles;
    [SerializeField] ParticleSystem DeathParticles;
    public int contactDamage;
    protected int speed;
    protected int lookDistance;
    private GameObject Player;
    
    public virtual void Start()
    {
    }

    public void Hit() 
    {
        // reduce health
        health--;

        // check if you're dead
        if (health <= 0)
        {
            // when dead, delete yourself
            Destroy(this.gameObject);
        }

    }

    public virtual bool CheckDistance()
    {
        if (this.gameObject.transform.position.x - FindObjectOfType<PlayerMovement>().gameObject.transform.position.x <= lookDistance)
        {
            return true;
        }
        return false;
    }

    public virtual void MoveToPlayer()
    {
        float whichWay = FindObjectOfType<PlayerMovement>().gameObject.transform.position.x - this.gameObject.transform.position.x;
        if (whichWay < 0)
        {
            this.gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else if (whichWay > 0)
        {
            this.gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().takeDamage(contactDamage);
        }
    }
}
