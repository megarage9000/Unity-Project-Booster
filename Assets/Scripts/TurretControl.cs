using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretControl : MonoBehaviour
{

    private bool canControlTurret = true;
    public GameObject projectile;
    [SerializeField] protected Transform turretBarrel;

    protected void FireTurret()
    {
        GameObject projectileInstance = Instantiate(projectile, turretBarrel.position, transform.rotation) as GameObject;
        Physics.IgnoreCollision(projectileInstance.GetComponent<Collider>(), GetComponent<Collider>());
    }

    protected abstract void RotateTurret();
    
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
}
