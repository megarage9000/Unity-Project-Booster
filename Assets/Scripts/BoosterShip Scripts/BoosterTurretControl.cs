using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterTurretControl : TurretControl
{

    [SerializeField] float mouseSensitivity = 5f;

    LayerMask castBackground;

    private void Awake()
    {
        castBackground = LayerMask.GetMask("MouseRayCast");
    }

    protected override void CalculateTurretRotation()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, castBackground))
        {
            Vector3 mouseToTurret = new Vector3(hit.point.x, hit.point.y) - new Vector3(turretBody.position.x, turretBody.position.y);
            mouseToTurret.z = 0;
            Quaternion newRotation = Quaternion.FromToRotation(new Vector3(turretBody.position.x, turretBody.position.y), mouseToTurret);
            RotateTurret(newRotation);
        }

    }

    protected override void FireTurret()
    {
        throw new System.NotImplementedException();
    }

    protected override void OperateTurret()
    {
        CalculateTurretRotation();
    }
}
