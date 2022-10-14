using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    //### FIELDS ###
    // oops all copied
    Camera cam;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    // own fields
    // private Vector3 upperLimit = new Vector3(0f, 0f, 0f);
    private Vector3 lowerLimit= new Vector3(0f, 2.2f, 0f);
    


    //### MAIN METHODS ###
    // Start, called before the game loads
    private void Start() 
    {
        cam = FindObjectOfType<Camera>();
    }

    // Update, called once a frame
    void Update ()
    {
        MoveCamera();
    }



    //### METHODS ###
    private void MoveCamera()
    {
        if (target)
        {
            Vector3 point = cam.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta + lowerLimit;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}
