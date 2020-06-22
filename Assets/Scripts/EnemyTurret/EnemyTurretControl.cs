using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretControl : TurretControl
{
    private const float TAU = Mathf.PI * 2;
    private const float OFFSET = 90f;
    private const string PLAYER_TAG = "Player";

    [SerializeField] float turretScanRange = 50f;
    [SerializeField] float turretScanWidth = 5f;
    [SerializeField] float maxRotationScan = 90f;
    [SerializeField] float scanSpeed = 1f;
    [SerializeField] float targetLockOnStrength = 1f;

    private bool isTargetFound;
    private BoxCollider turretScanArea;
    private Quaternion leftRotationBound;
    private Quaternion rightRotationBound;

    public override void Awake()
    {
        base.Awake();
        turretScanArea = GetComponent<BoxCollider>();

        Vector3 initialAreaCenter = turretScanArea.center;
        Vector3 initialAreaSize = turretScanArea.size;

        turretScanArea.size = new Vector3(turretScanWidth, turretScanRange, initialAreaSize.z);
        turretScanArea.center = new Vector3(initialAreaCenter.x, initialAreaCenter.y + turretScanRange / 2, initialAreaCenter.z);

        leftRotationBound = Quaternion.Euler(0, 0, transform.rotation.z + -maxRotationScan);
        rightRotationBound = Quaternion.Euler(0, 0, transform.rotation.z + maxRotationScan);

        scanSpeed = 1 / scanSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;

        if (tag == PLAYER_TAG)
        {
            isTargetFound = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string tag = other.gameObject.tag;

        if (tag == PLAYER_TAG)
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
    // And from the Unity Lesson of course
    private void scan()
    {
        float cycles = Time.time / scanSpeed;
        float sin = Mathf.Sin(cycles * TAU);
        float rotationFactor = sin / 2f + 0.5f;
        transform.rotation = Quaternion.Slerp(leftRotationBound, rightRotationBound, rotationFactor);
    }

    // Used similar algorithm to booster control
    private void target()
    {
        GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
        Vector3 targetPosition = player.transform.position;
        Vector2 directionToLook = new Vector2(targetPosition.x, targetPosition.y) - new Vector2(transform.position.x, transform.position.y);
        float angleZ = Mathf.Atan2(directionToLook.y, directionToLook.x) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.Euler(new Vector3(0f, 0f, angleZ - OFFSET));
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, targetLockOnStrength * Time.smoothDeltaTime);
        FireTurret();
    }

}
