using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretControl : MonoBehaviour
{

    [SerializeField] private Transform turretBarrel;
    [SerializeField] private int projectilesPerSecond = 1;

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

    public void instantiateProjectile()
    {
        GameObject projectileInstance = Instantiate(projectile, turretBarrel.position, transform.rotation) as GameObject;
        Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
        AudioClip fireSound = projectileScript.GetFireSound();
        projectileScript.GetComponent<Projectile>().Fire();
        Physics.IgnoreCollision(projectileInstance.GetComponent<Collider>(), GetComponent<Collider>());
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
