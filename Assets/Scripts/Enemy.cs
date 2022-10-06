using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int contactDamage;
    [SerializeField] ParticleSystem HitParticles;
    [SerializeField] ParticleSystem DeathParticles;

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
}
