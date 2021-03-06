﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterControl : MonoBehaviour
{

    private const int LEFT_DIRECTION = 1;
    private const int RIGHT_DIRECTION = -1;

    [SerializeField] public ParticleSystem paralysisEffect;

    [SerializeField] float rcsRotationSpeed = 100f;
    [SerializeField] float rcsThrusterSpeed = 100f;
    [SerializeField] float rcsGravityModifier = 5f;
    [SerializeField] Rigidbody rocketShipBody;
    [SerializeField] AudioClip thrusterNoise;
    [SerializeField] ParticleSystem leftJetParticles;
    [SerializeField] ParticleSystem rightJetParticles;

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
        else
        {
            stopParticles();
        }
        rocketShipBody.AddForce(Vector3.down * rcsGravityModifier * Time.deltaTime, ForceMode.Force);
    }

    public void CheckThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
            if (!audio.isPlaying)
            {
                audio.PlayOneShot(thrusterNoise);
                playParticles();
            }
            
        }
        else
        {
            
            audio.Stop();
            stopParticles();
        }
    }

    public void CheckRotationInput()
    {
        rocketShipBody.angularVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(LEFT_DIRECTION);
        }
        if (Input.GetKey(KeyCode.D))
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
        rocketShipBody.AddForce(this.gameObject.transform.up * thrustAtFrame, ForceMode.VelocityChange);
    }

    private void playParticles()
    {
        leftJetParticles.Play();
        rightJetParticles.Play();
    }

    private void stopParticles()
    {
        leftJetParticles.Stop();
        rightJetParticles.Stop();
    }

    public void TerminateThrusterControl()
    {
        StopAllCoroutines();
        DisableThrusterControl();
    }
    public void DisableThrusterControl()
    {
        canControlThrust = false;
        audio.Stop();
    }

    public void EnableThrusterControl()
    {
        canControlThrust = true;
    }

    public void InitializeParalyzeEffect(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(ParalyzeThrust(duration));
        paralysisEffect.Play();
    }

    IEnumerator ParalyzeThrust(float duration)
    {
        DisableThrusterControl();
        yield return new WaitForSeconds(duration);
        EnableThrusterControl();
        paralysisEffect.Stop();
    }




}
