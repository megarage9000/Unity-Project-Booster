using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{

    private const string ENEMY_TAG = "Enemy";

    private const float DEFAULT_DMG = 10f;
    private float explosionDamage;
    private float explosionDuration;
    private bool canHitEnemy = false;
    private ParticleSystem explosionParticles;
    private SphereCollider sphereCollider;
    
    private void Awake()
    {
        explosionDamage = DEFAULT_DMG;
        explosionParticles = GetComponent<ParticleSystem>();
        sphereCollider = GetComponent<SphereCollider>();
        explosionDuration = explosionParticles.main.duration;
    }
    public void StartExplosion()
    {
        canHitEnemy = true;
        explosionParticles.Play();
        Destroy(gameObject, explosionDuration);
    }
    public void SetDamage(float damage)
    {
        explosionDamage = damage;
    }

    private float GetDamage()
    {
        return explosionDamage;
    }

    public void SetExplosionRadius(float radius)
    {
        sphereCollider.radius = radius;
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject detectedObj = other.gameObject;
        string tag = other.gameObject.tag;
        if (tag.Equals(ENEMY_TAG) && canHitEnemy == true)
        {
            canHitEnemy = false;
            detectedObj.GetComponent<EnemyTurretScript>().DamageTurret(GetDamage());
            
        }
        
    }

}
