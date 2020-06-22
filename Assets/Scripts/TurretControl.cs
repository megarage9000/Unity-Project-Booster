using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretControl : MonoBehaviour
{
    const int MINUTE = 60;

    [SerializeField] protected Transform turretBarrel;
    [SerializeField] private int projectilesPerSecond;

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
            GameObject projectileInstance = Instantiate(projectile, turretBarrel.position, transform.rotation) as GameObject;
            AudioClip fireSound = projectileInstance.GetComponent<Projectile>().GetFireSound();
            projectileInstance.GetComponent<Projectile>().Fire();
            Physics.IgnoreCollision(projectileInstance.GetComponent<Collider>(), GetComponent<Collider>());
            audio.PlayOneShot(fireSound);
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

    public void DisableTurretControl()
    {
        canControlTurret = false;
        StopCoroutine(RateOfFireController());
        audio.Stop();
    }

    IEnumerator RateOfFireController()
    {
        canFireTurret = false;
        yield return new WaitForSeconds(1 / projectilesPerSecond);
        canFireTurret = true;

    }
}
