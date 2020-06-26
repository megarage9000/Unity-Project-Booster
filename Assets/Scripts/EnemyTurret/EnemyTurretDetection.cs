using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTurretDetection : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    public UnityEvent onDetect;
    public UnityEvent onLeave;

    [SerializeField] float turretScanRange = 50f;
    [SerializeField] float turretScanWidth = 5f;

    private BoxCollider turretScanArea;
    // Start is called before the first frame update
    private void Awake()
    {
        turretScanArea = GetComponent<BoxCollider>();

        Vector3 initialAreaCenter = turretScanArea.center;
        Vector3 initialAreaSize = turretScanArea.size;

        turretScanArea.size = new Vector3(turretScanWidth, turretScanRange, initialAreaSize.z);
        turretScanArea.center = new Vector3(initialAreaCenter.x, initialAreaCenter.y + turretScanRange / 2f, initialAreaCenter.z);
    }

    public void OnTriggerEnter(Collider other)
    {
        string objectTag = other.gameObject.tag;
        if (objectTag.Equals(PLAYER_TAG))
        {
            onDetect.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        string objectTag = other.gameObject.tag;
        if (objectTag.Equals(PLAYER_TAG))
        {
            onLeave.Invoke();
        }
    }
}
