using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretControl : MonoBehaviour
{

    private bool canControlTurret = true;
    private bool canFireTurret = true;
    public GameObject projectile;
    [SerializeField] protected Transform turretBarrel;
    [SerializeField] private int projectilesPerSecond;

    protected void FireTurret()
    {
        if (canFireTurret)
        {
            GameObject projectileInstance = Instantiate(projectile, turretBarrel.position, transform.rotation) as GameObject;
            Physics.IgnoreCollision(projectileInstance.GetComponent<Collider>(), GetComponent<Collider>());
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
    }

    IEnumerator RateOfFireController()
    {
        canFireTurret = false;
        yield return new WaitForSeconds(1 / projectilesPerSecond);
        canFireTurret = true;
        yield return null;
    }
}
