using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        health = 5;
        contactDamage = 2;
        speed = 3;
        lookDistance = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckDistance())
        {
            // Debug.Log("I see you");
            MoveToPlayer();
        }
    }
}
