using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpion : Enemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        health = 3;
        contactDamage = 1;
        speed = 1.9f;
        lookDistance = 12;
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
