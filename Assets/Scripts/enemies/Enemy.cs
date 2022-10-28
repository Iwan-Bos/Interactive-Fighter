using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int health;
    [SerializeField] ParticleSystem HitParticles;
    [SerializeField] ParticleSystem DeathParticles;
    public int contactDamage;
    protected float speed;
    protected int lookDistance;
    private GameObject Player;
    private protected float playerTimeout;
    
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
        playerTimeout += Time.deltaTime;
        float distance = this.gameObject.transform.position.x - FindObjectOfType<PlayerMovement>().gameObject.transform.position.x;
        if (distance <= lookDistance && distance >= -lookDistance)
        {
            return true;
        }
        else
        {
            playerTimeout = 2;
            return false;
        }
    }

    public virtual void MoveToPlayer()
    {
        float whichWay = FindObjectOfType<PlayerMovement>().gameObject.transform.position.x - this.gameObject.transform.position.x;
        if (whichWay < -0.1 && playerTimeout > 1.5f)
        {
            this.gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else if (whichWay > 0.1 && playerTimeout > 1.5f)
        {
            this.gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else if (playerTimeout > 1.5f)
        {
            playerTimeout = 0;
        }
    }
    public virtual void FlyToPlayer()
    {
        Vector3 whichWay = FindObjectOfType<PlayerMovement>().gameObject.transform.position - this.gameObject.transform.position;
        if ((whichWay.x < -0.1 || whichWay.x > 0.1) && playerTimeout > 1.5f)
        {
            Vector3 movement = Vector3.Normalize(whichWay);
            this.gameObject.transform.position += movement * Time.deltaTime * speed;
        }
        else if (playerTimeout > 1.5f)
        {
            playerTimeout = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().takeDamage(contactDamage);
        }
    }
}
