using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretControl : MonoBehaviour
{

    private bool canControlTurret = true;
    [SerializeField] protected Transform turretBarrel;
    [SerializeField] protected Transform turretBody;

    protected void RotateTurret(Quaternion newRotation)
    {
        turretBody.rotation = newRotation;
        Debug.Log("Rotation of turret: " + turretBody.rotation);
    }

    protected abstract void CalculateTurretRotation();
    protected abstract void FireTurret();
    protected abstract void OperateTurret();

    private void Update()
    {
        if (canControlTurret)
        {
            OperateTurret();
        }   
    }

    public void disableTurretControl()
    {
        canControlTurret = false;
    }
}
