using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretControl : TurretControl
{

    [SerializeField] float turretScanRange = 50f;
    BoxCollider turretScanArea;

    private void Awake()
    {
        turretScanArea = GetComponent<BoxCollider>();

        Vector3 initialAreaCenter = turretScanArea.center;
        Vector3 initialAreaSize = turretScanArea.size;

        turretScanArea.center.Set(initialAreaCenter.x, initialAreaCenter.y + turretScanRange / 2, initialAreaCenter.z);
        turretScanArea.size.Set(initialAreaSize.x, turretScanRange, initialAreaSize.z);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Detected an object!");
    }
    protected override void OperateTurret()
    {
           
    }

    protected override void RotateTurret()
    {
        
    }

    private void scan()
    {

    }

    

}
