using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterTurretControl : TurretControl
{
    private const float ANGLE_OFFSET = 90f;
    [SerializeField] float mouseSensitivity = 5f;
    [SerializeField] AudioClip turretNoise;
  
    private Camera camera;
    private AudioSource audio;

    private void Awake()
    {
        camera = Camera.main; 
        audio = GetComponent<AudioSource>();
    }

    private void RespondToCLick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audio.PlayOneShot(turretNoise);
            FireTurret();
        }
    }

    protected override void OperateTurret()
    {
        RotateTurret();
        RespondToCLick();
    }

    protected override void RotateTurret()
    {
        Vector2 transformScreenPosition = camera.WorldToScreenPoint(transform.position);
        Vector2 targetPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        Vector2 directionToLook = targetPosition - transformScreenPosition;
        float angleZ = Mathf.Atan2(directionToLook.y, directionToLook.x) * Mathf.Rad2Deg;

        Quaternion newRotation = Quaternion.Euler(new Vector3(0f, 0f, angleZ - ANGLE_OFFSET));
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * mouseSensitivity); 
    }

    public void DisableTurretControl()
    {
        base.DisableTurretControl();
        audio.Stop();
    }
}
