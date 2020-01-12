using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update

    //Components of rocket
    private Rigidbody rigidBody;
    private AudioSource audio;


    //Fields for use
    Vector3 rotation;
    float angle = 2;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();   
    }

    // Handles the input of the Rocket
    // - Rotation and thrusters
    private void ProcessInput()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audio.isPlaying)
            {
                audio.Play();
            }
            
            Debug.Log("Rocket Thruster initiated");
        }
        else
        {
            audio.Stop();
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
