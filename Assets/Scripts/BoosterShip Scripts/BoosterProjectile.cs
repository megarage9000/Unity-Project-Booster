using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class BoosterProjectile : Projectile
{
    [SerializeField] int projectileDamage = 25;

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
        
        boosterProjectileParticles.Play();
        Destroy(gameObject, durationOfParticleEffect);
    }

    public override void OnFire()
    {
        Debug.Log("Booster Projectile Fired!");
    }

    public int GetDamage()
    {
        return projectileDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        Debug.Log("Got " + tag);
        projectile.SetActive(false);
        rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        OnDelete();
    }

}
