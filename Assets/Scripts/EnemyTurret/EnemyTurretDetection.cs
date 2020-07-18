using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTurretDetection : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const float MAX_DISTANCE = Mathf.Infinity;

    public UnityEvent onDetect;
    public UnityEvent onLeave;

    [SerializeField] float turretScanRange = 50f;
    [SerializeField] float turretScanWidth = 5f;

    private BoxCollider turretScanArea;
    private HashSet<GameObject> detectedObjects;
    private string nearestObjectTag = "";

    private void Awake()
    {
        detectedObjects = new HashSet<GameObject>();
        turretScanArea = GetComponent<BoxCollider>();

        Vector3 initialAreaCenter = turretScanArea.center;
        Vector3 initialAreaSize = turretScanArea.size;

        turretScanArea.size = new Vector3(turretScanWidth, turretScanRange, initialAreaSize.z);
        turretScanArea.center = new Vector3(initialAreaCenter.x, initialAreaCenter.y + turretScanRange / 2f, initialAreaCenter.z);
        
    }

    

    public void OnTriggerEnter(Collider other)
    {
        
        detectedObjects.Add(other.gameObject);
        if (isPlayerClosest())
        {
            onDetect.Invoke();
        }
        else
        {
            onLeave.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        detectedObjects.Remove(other.gameObject);
        if (isPlayerClosest())
        {
            onDetect.Invoke();
        }
        else {
            onLeave.Invoke();
        }
       
    }
  
  

    // Finding closest enemy
    // https://answers.unity.com/questions/1236558/finding-nearest-game-object.html
    private bool isPlayerClosest()
    {
        float minDistance = MAX_DISTANCE;
        string closestObjectTag = "";

        foreach (GameObject obj in detectedObjects)
        {
            if(obj != null)
            {

                Vector3 distance = transform.position - obj.transform.position;
                if(minDistance > distance.magnitude)
                {
                    minDistance = distance.magnitude;
                    closestObjectTag = obj.tag;
                }
            }
        }

        return (PLAYER_TAG.Equals(closestObjectTag));
    }
    

    
}
