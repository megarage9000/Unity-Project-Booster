﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterTurretControl : TurretControl
{
    [SerializeField] public ParticleSystem paralysisEffect;

    private const float ANGLE_OFFSET = 90f;
    [SerializeField] float mouseSensitivity = 5f;
  
    private Camera camera;

    public override void Awake()
    {
        base.Awake();
        camera = Camera.main; 
    }

    private void RespondToCLick()
    {
        if (Input.GetMouseButton(0))
        {
            FireTurret();
        }
    }

    protected override void OperateTurret()
    {
        RotateTurret();
        RespondToCLick();
    }

    protected void RotateTurret()
    {
        Vector2 transformScreenPosition = camera.WorldToScreenPoint(transform.position);
        Vector2 targetPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        Vector2 directionToLook = targetPosition - transformScreenPosition;
        float angleZ = Mathf.Atan2(directionToLook.y, directionToLook.x) * Mathf.Rad2Deg;

        Quaternion newRotation = Quaternion.Euler(new Vector3(0f, 0f, angleZ - ANGLE_OFFSET));
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * mouseSensitivity); 
    }

    public void InitializeParalyzeEffect(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(ParalyzeTurret(duration));
        paralysisEffect.Play();
    }

    public void TerminateBoosterTurretControl()
    {
        StopAllCoroutines();
        DisableTurretControl();
    }
    IEnumerator ParalyzeTurret(float duration)
    {
        DisableTurretControl();
        yield return new WaitForSeconds(duration);
        EnableTurretControl();
        paralysisEffect.Stop();
    }
}
