using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
{
    // # FIELDS #
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Color color = Color.red;
    


    // # METHODS #
    // attack hibox using OverlapBox(), returns all hit colliders 
    public Collider[] OverlapBox()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, enemyLayer);

        return hitColliders;
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing.
    void OnDrawGizmos()
    {
        // set box color
        Gizmos.color = color;
        
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
