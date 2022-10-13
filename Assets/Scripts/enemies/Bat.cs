using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        health = 2;
        contactDamage = 1;
        speed = 3.5f;
        lookDistance = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckDistance())
        {
            // Debug.Log("I see you");
            FlyToPlayer();
        }
    }
}
