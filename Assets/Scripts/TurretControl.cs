using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretControl : MonoBehaviour
{

    private bool canControlTurret = true;
    [SerializeField] protected Transform turretBarrel;

    protected abstract void RotateTurret();
    protected abstract void FireTurret();
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
