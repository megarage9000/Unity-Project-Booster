using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class BoosterProjectile : Projectile
{

    const string ENEMY_TAG = "Enemy";

    [SerializeField] int projectileDamage = 25;

    public GameObject boosterProjectileParticles;
    public GameObject projectile;
 
    private float durationOfParticleEffect;
    private Rigidbody rigidbody;

    public override void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody>();

    }
    public override void OnDelete()
    {
        GameObject particles = Instantiate(boosterProjectileParticles, transform.position, Quaternion.identity) as GameObject;
        ParticleSystem particleSys = particles.GetComponent<ParticleSystem>();
        durationOfParticleEffect = particleSys.main.duration;
        particleSys.Play();
        Destroy(particles, durationOfParticleEffect);
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
        GameObject detectedObj = collision.gameObject;
        string tag = collision.gameObject.tag;
        if (tag.Equals(ENEMY_TAG))
        {
            detectedObj.GetComponent<EnemyTurretScript>().DamageTurret(GetDamage());   
        }
        projectile.SetActive(false);
        OnDelete();
    }

}
