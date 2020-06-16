﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretControl : TurretControl
{
    const float TAU = Mathf.PI * 2;
    private const float OFFSET = 90f;
    [SerializeField] float turretScanRange = 50f;
    [SerializeField] float maxRotationScan = 90f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float period = 1f;

    BoxCollider turretScanArea;
    private bool isTargetFound;

    private Quaternion leftRotationBound;
    private Quaternion rightRotationBound;
    private float ANGLE_OFFSET = 90f;

    private void Awake()
    {
        turretScanArea = GetComponent<BoxCollider>();

        Vector3 initialAreaCenter = turretScanArea.center;
        Vector3 initialAreaSize = turretScanArea.size;

        turretScanArea.size = new Vector3(initialAreaSize.x, turretScanRange, initialAreaSize.z);
        turretScanArea.center = new Vector3(initialAreaCenter.x, initialAreaCenter.y + turretScanRange / 2, initialAreaCenter.z);

        leftRotationBound = Quaternion.Euler(0, 0, transform.rotation.z + -maxRotationScan);
        rightRotationBound = Quaternion.Euler(0, 0, transform.rotation.z + maxRotationScan);
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 detectedObjectPos = other.gameObject.transform.position;
        string tag = other.gameObject.tag;

        if (tag == "Player")
        {
            isTargetFound = true;
        }

        Debug.Log("Found object with tag: " + tag + " at position: " + detectedObjectPos);
    }

    private void OnTriggerExit(Collider other)
    {
        string tag = other.gameObject.tag;

        if (tag == "Player")
        {
            isTargetFound = false;
        }
    }
    protected override void OperateTurret()
    {
        if (isTargetFound)
        {
            target();
        }
        else
        {
            scan();
        }
    }

    // Oscillating effect from here:
    // https://answers.unity.com/questions/822484/rotate-object-over-time-and-oscillate.html
    private void scan()
    {
        float cycles = Time.time / period;
        float sin = Mathf.Sin(cycles * TAU);
        float rotationFactor = sin / 2f + 0.5f;
        transform.rotation = Quaternion.Slerp(leftRotationBound, rightRotationBound, rotationFactor);
    }

    // Used similar algorithm to booster control
    private void target()
    {
        Vector3 targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 directionToLook = new Vector2(targetPosition.x, targetPosition.y) - new Vector2(transform.position.x, transform.position.y);

        float angleZ = Mathf.Atan2(directionToLook.y, directionToLook.x) * Mathf.Rad2Deg;
        
        Quaternion newRotation = Quaternion.Euler(new Vector3(0f, 0f, angleZ - OFFSET));
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }

    

}