using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class BoosterProjectile : Projectile
{

    private const string ENEMY_TAG = "Enemy";

    [SerializeField] float projectileDamage = 25;
    [SerializeField] float splashDamage = 10f;
    [SerializeField] float splashRadius = 1.5f;

    public GameObject boosterProjectileParticles;
    public GameObject projectile;

    private bool hasHitEnemy = false;

    
    public override void OnDelete()
    {
        base.OnDelete();
        startExplosion();
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return projectileDamage;
    }

    private void startExplosion()
    {
        GameObject particles = Instantiate(boosterProjectileParticles, transform.position, Quaternion.identity) as GameObject;
        ExplosionScript splashDamageScript = particles.GetComponent<ExplosionScript>();
        splashDamageScript.SetDamage(splashDamage);
        splashDamageScript.SetExplosionRadius(splashRadius);
        splashDamageScript.StartExplosion();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject detectedObj = collision.gameObject;
        string tag = detectedObj.tag;
        
        if (tag.Equals(ENEMY_TAG) && hasHitEnemy == false)
        {
            hasHitEnemy = true;
            detectedObj.GetComponent<EnemyTurretScript>().DamageTurret(GetDamage());
        }
        projectile.SetActive(false);
        OnDelete();
    }

}
