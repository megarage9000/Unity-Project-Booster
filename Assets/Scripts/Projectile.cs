using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    private const float DEFAULT_RANGE = 100f;
    private const float DEFAULT_SPEED = 100f;

    [SerializeField] AudioClip onFireSound;

    private Rigidbody bulletBody;
    private float projectileSpeed;
    private float maxProjectileRange;
    private Vector3 initialPosition;

    public void Awake()
    {
        bulletBody = GetComponent<Rigidbody>();
        SetupProjectile();
        initialPosition = transform.position;
    }

    public AudioClip GetFireSound()
    {
        return onFireSound;
    }

    public void Fire()
    {
        OnFire();
        bulletBody.velocity = transform.up * projectileSpeed;
        float calculatedDistance = CalculateProjectileDistance();
        // Debug.Log("Speed = " + projectileSpeed + ", Range = " + calculatedDistance);
        StartCoroutine(DeleteProjectileInstance(calculatedDistance));
    }

    public void SetMaxProjectileRange(float range){ maxProjectileRange = range;}

    public void SetProjectileSpeed(float speed){ projectileSpeed = speed;}

    public abstract void OnFire();

    public abstract void OnDelete();

    public abstract void SetupProjectile();

    IEnumerator DeleteProjectileInstance(float distance)
    {
        while(Vector3.Distance(transform.position, initialPosition) < distance)
        {
            yield return null;
        }

        OnDelete();
    }

    private float CalculateProjectileDistance()
    {
        int layerMask = LayerMask.GetMask("TurretScanRange");
        layerMask = ~layerMask;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, maxProjectileRange, layerMask))
        {
            return hit.distance;
        }
        else
        {
            return maxProjectileRange;
        }
    }

}
