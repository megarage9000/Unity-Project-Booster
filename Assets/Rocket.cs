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
    [SerializeField] float rcsGravityModifier = 5f;
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

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly!");
                break;

            default:
                Debug.Log("Dead");
                break;

        }
    }

    // Handles the input of the Rocket
    // - Rotation and thrusters
    private void Rotate()
    {
        //Freeze rotation prior to rotation
        rigidBody.freezeRotation = true;

        //Calculating rotation per frame with deltaTime for consistent rotation
        float rotationAtFrame = rcsRotationSpeed * Time.deltaTime; 

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

        //Enabling rotation after rotation has been added
        rigidBody.freezeRotation = false; 

    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            //Calculating thrust per frame with deltaTime for conisitent thrusting
            float thrustAtFrame = rcsThrusterSpeed * Time.deltaTime;

            rigidBody.AddRelativeForce(Vector3.up * thrustAtFrame, ForceMode.Impulse);

            //play thrust audio on press
            if (!audio.isPlaying) 
            {
                audio.Play(); 
            }

            Debug.Log("Rocket Thruster initiated");
        }
        else
        {
            //Reducing gravity by adding a constant opposite force
            rigidBody.AddRelativeForce(Vector3.up * rcsGravityModifier * Time.deltaTime, ForceMode.Acceleration);
            audio.Stop();
        }
    }
}
