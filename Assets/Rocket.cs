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


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Thrust();
    }

    // Handles the input of the Rocket
    // - Rotation and thrusters
    private void Rotate()
    {
        rigidBody.freezeRotation = true; //Freeze rotation prior to rotation

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

        rigidBody.freezeRotation = false; //Enabling rotation after rotation has been added

    }

    private void Thrust()
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
    }
}
