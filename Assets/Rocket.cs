﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigidBody;
    int multiplier = 5;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();   
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * multiplier);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddRelativeForce(Vector3.right * -multiplier);
            Debug.Log("Rotate Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddRelativeForce(Vector3.right * multiplier);
            Debug.Log("Rotate Right");
        }
    }
}
