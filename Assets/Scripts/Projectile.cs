using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    Rigidbody bulletBody;
    float projectileSpeed;

    private void Awake()
    {
        bulletBody = GetComponent<Rigidbody>();
        projectileSpeed = 10f;
    }

    public void Fire()
    {
        bulletBody.AddRelativeForce(transform.up * projectileSpeed);
    }

    public void SetProjectileExpiration(float timeUntilDelete)
    {
        Destroy(gameObject, timeUntilDelete);
    }

    public void SetProjectileSpeed(float speed)
    {
        projectileSpeed = speed;
    }

}
