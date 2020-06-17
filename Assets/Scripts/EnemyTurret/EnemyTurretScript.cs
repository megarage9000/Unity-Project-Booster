using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretScript : MonoBehaviour
{
    const string PLAYER_PROJECTILE_TAG = "Player Projectile";

    [SerializeField] GameObject turret;
    [SerializeField] float timeUntilActiveTurret = 3f;

    void Awake()
    {
        if (turret.activeInHierarchy)
        {
            turret.SetActive(false);
        }
        StartCoroutine(Activation());
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if(tag == PLAYER_PROJECTILE_TAG)
        {
            Debug.Log("Hit by player!");
        }
    }

    IEnumerator Activation()
    {
        yield return new WaitForSeconds(timeUntilActiveTurret);
        turret.SetActive(true);

    }
}
