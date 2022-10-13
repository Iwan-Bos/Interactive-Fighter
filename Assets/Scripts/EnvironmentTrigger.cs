using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTrigger : MonoBehaviour
{
    [SerializeField] Color color = Color.green;
    public int triggerType = 0; 
    /* the trigger type
     * 0 = darkness 
     * 1 = cold
     */
    
    //Draw the Trigger as a gizmo to show where it currently is.
    void OnDrawGizmos()
    {
        // set box color
        Gizmos.color = color;
        
        //Draw a cube where the Trigger is
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    public void AddDarkness()
    {
        
    }
    public void AddColdness()
    {

    }
}