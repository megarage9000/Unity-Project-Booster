using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    private const float DEFAULT_RANGE = 100f;
    private const float DEFAULT_SPEED = 100f;

    // readonly array: https://stackoverflow.com/questions/14063203/const-array-of-strings
    private readonly string[] IGNORED_LAYERS = { "TurretScanRange", "Projectiles", "PlayerAndEnemies" };

    [SerializeField] AudioClip onFireSound;
    [SerializeField] float maxProjectileRange = DEFAULT_RANGE;
    [SerializeField] float projectileSpeed = DEFAULT_SPEED;

    private Rigidbody bulletBody;
    private Vector3 initialPosition;

    public virtual void Awake()
    {
        bulletBody = GetComponent<Rigidbody>();
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
        StartCoroutine(DeleteProjectileInstance(calculatedDistance));
    }

    public abstract void OnFire();

    public virtual void OnDelete()
    {
        bulletBody.velocity = Vector3.zero;
        bulletBody.constraints = RigidbodyConstraints.FreezePosition;
    }

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
        int layerMask = LayerMask.GetMask(IGNORED_LAYERS);
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
