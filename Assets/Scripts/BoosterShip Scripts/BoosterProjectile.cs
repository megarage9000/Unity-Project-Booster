using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class BoosterProjectile : Projectile
{

    const string PLAYER_TAG = "Player";

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
        Debug.Log("Booster Projectile Deleted!");

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
        string tag = collision.gameObject.tag;
        Debug.Log("Got " + tag);
        if (!tag.Equals(PLAYER_TAG))
        {
            projectile.SetActive(false);
            rigidbody.constraints = RigidbodyConstraints.FreezePosition;
            OnDelete();
        }
    }

}
