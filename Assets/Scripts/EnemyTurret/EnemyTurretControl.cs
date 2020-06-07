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

        turretScanArea.size = new Vector3(initialAreaSize.x, turretScanRange, initialAreaSize.z);
        turretScanArea.center = new Vector3(initialAreaCenter.x, initialAreaCenter.y + turretScanRange / 2, initialAreaCenter.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 detectedObjectPos = other.gameObject.transform.position;
        string tag = other.gameObject.tag;

        Debug.Log("Found object with tag: " + tag + " at position: " + detectedObjectPos);
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

    private void target()
    {

    }

    

}
