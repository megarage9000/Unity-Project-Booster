using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretControl : MonoBehaviour
{

    [SerializeField] private Transform turretBarrel;
    [SerializeField] private int projectilesPerSecond = 1;
    [SerializeField] private float spread = 0;

    private bool canControlTurret = true;
    private bool canFireTurret = true;
    private AudioSource audio;
    public GameObject projectile;
    

    public virtual void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    protected void FireTurret()
    {
        if (canFireTurret)
        {
            StartCoroutine(RateOfFireController());
        }
        
    }
    
    protected abstract void OperateTurret();

    public void Update()
    {
        if (canControlTurret)
        {
            OperateTurret();
        }   
    }

    private Quaternion CalculateProjectileSpread()
    {
        float randomZ = UnityEngine.Random.Range(-spread/2f, spread/2f);
        Quaternion originalRotation = transform.rotation;
        return Quaternion.Euler(
            originalRotation.eulerAngles.x, 
            originalRotation.eulerAngles.y, 
            originalRotation.eulerAngles.z + randomZ);
    }
    public void instantiateProjectile()
    {
        Quaternion angle = CalculateProjectileSpread();
        GameObject projectileInstance = Instantiate(projectile, turretBarrel.position, angle) as GameObject;
        Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
        AudioClip fireSound = projectileScript.GetFireSound();
        Physics.IgnoreCollision(
            projectileInstance.GetComponent<Collider>(),
            GetComponent<Collider>());
        projectileScript.Fire();
        audio.PlayOneShot(fireSound);
    }
    public void DisableTurretControl()
    {
        canControlTurret = false;
        StopCoroutine(RateOfFireController());
        audio.Stop();
    }

    IEnumerator RateOfFireController()
    {
        canFireTurret = false;
        instantiateProjectile();
        float delay = 1f / projectilesPerSecond;
        yield return new WaitForSeconds(delay);
        canFireTurret = true;

    }
}
