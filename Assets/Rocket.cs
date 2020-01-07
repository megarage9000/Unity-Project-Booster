using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigidBody;
    Transform transform;
    int multiplier = 5;
    Vector3 rotation;
    float angle = 2;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();   
    }

    private void ProcessInput()
    {
        rotation = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * multiplier);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
            Debug.Log("Rotate Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
            Debug.Log("Rotate Right");
        }
        
    }
}
