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

    [SerializeField] float rcsRotationSpeed = 100f;
    [SerializeField] float rcsThrusterSpeed = 100f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
        
    }

    // Handles the input of the Rocket
    // - Rotation and thrusters
    private void Rotate()
    {
        rigidBody.freezeRotation = true; //Freeze rotation prior to rotation

        float rotationAtFrame = rcsRotationSpeed * Time.deltaTime; //Calculating rotation per frame with deltaTime for consistent rotation

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationAtFrame);
            Debug.Log("Rotate Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationAtFrame);
            Debug.Log("Rotate Right");
        }

        rigidBody.freezeRotation = false; //Enabling rotation after rotation has been added

    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float thrustAtFrame = rcsThrusterSpeed * Time.deltaTime; //Calculating thrust per frame with deltaTime for conisitent thrusting

            rigidBody.AddRelativeForce(Vector3.up * thrustAtFrame);

           
            if (!audio.isPlaying) //play thrust audio on press
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
