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
    private AudioSource audio;

    public void Awake()
    {
        bulletBody = GetComponent<Rigidbody>();
        projectileSpeed = DEFAULT_SPEED;
        projectileRange = DEFAULT_RANGE;
        initialPosition = transform.position;
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SetupProjectile();
    }

    public void Fire()
    {
        OnFire();
        bulletBody.velocity = transform.up * projectileSpeed;
        StartCoroutine(DeleteProjectileInstance(projectileRange));
        audio.PlayOneShot(onFireSound);
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
