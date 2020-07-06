using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class BoosterProjectile : Projectile
{
    public ParticleSystem boosterProjectileParticles;
    public GameObject projectile;
    private float durationOfParticleEffect;
    private Rigidbody rigidbody;

    public override void Awake()
    {
        base.Awake();
        durationOfParticleEffect = boosterProjectileParticles.main.duration;
        rigidbody = GetComponent<Rigidbody>();

    }
    public override void OnDelete()
    {
        Debug.Log("Booster Projectile Deleted!");
        rigidbody.velocity = new Vector3(0, 0, 0);
        boosterProjectileParticles.Play();
        Destroy(gameObject, durationOfParticleEffect);
    }

    public override void OnFire()
    {
        Debug.Log("Booster Projectile Fired!");
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        Debug.Log("Got " + tag);
        projectile.SetActive(false);
        OnDelete();
    }

}
