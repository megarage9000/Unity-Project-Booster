using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterControl : MonoBehaviour
{

    private const int LEFT_DIRECTION = 1;
    private const int RIGHT_DIRECTION = -1;

    [SerializeField] float rcsRotationSpeed = 100f;
    [SerializeField] float rcsThrusterSpeed = 100f;
    [SerializeField] float rcsGravityModifier = 5f;
    [SerializeField] Rigidbody rocketShipBody;
    [SerializeField] AudioClip thrusterNoise;

    private bool canControlThrust;
    private AudioSource audio;

    public void Start()
    {
        canControlThrust = true;
        audio = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (canControlThrust)
        {
            CheckThrustInput();
            CheckRotationInput();
        }
    }

    public void CheckThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
            if (!audio.isPlaying)
            {
                audio.PlayOneShot(thrusterNoise);
            }
            
        }
        else
        {
            rocketShipBody.AddForce(Vector3.down * rcsGravityModifier * Time.deltaTime, ForceMode.Acceleration);
            audio.Stop();
        }
    }

    public void CheckRotationInput()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(LEFT_DIRECTION);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(RIGHT_DIRECTION);
        }

        //Enabling rotation after rotation has been added
        
    }

    private void ApplyRotation(int direction)
    {
        //Calculating rotation per frame with deltaTime for consistent rotation
        float rotationAtFrame = rcsRotationSpeed * Time.deltaTime;
        transform.Rotate(direction * Vector3.forward * rotationAtFrame);
       
    }

    private void ApplyThrust()
    {
        //Calculating thrust per frame with deltaTime for conisitent thrusting
        float thrustAtFrame = rcsThrusterSpeed * Time.deltaTime;
        rocketShipBody.AddRelativeForce(transform.up * thrustAtFrame, ForceMode.Impulse);
    }

 

    public void DisableThrusterControl()
    {
        canControlThrust = false;
        audio.Stop();
    }

}
