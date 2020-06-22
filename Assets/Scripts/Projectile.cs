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
    private float projectileRange;
    private Vector3 initialPosition;

    public void Awake()
    {
        bulletBody = GetComponent<Rigidbody>();
        projectileSpeed = DEFAULT_SPEED;
        projectileRange = DEFAULT_RANGE;
        initialPosition = transform.position;
    }

    private void Start()
    {
        SetupProjectile();
    }

    public AudioClip GetFireSound()
    {
        return onFireSound;
    }

    public void Fire()
    {
        OnFire();
        bulletBody.velocity = transform.up * projectileSpeed;
        StartCoroutine(DeleteProjectileInstance(projectileRange));
    }

    public void SetProjectileRange(float range){ projectileRange = range;}

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

}
