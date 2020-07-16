using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretScript : MonoBehaviour
{
    const string PLAYER_PROJECTILE_TAG = "Player Projectile";
    private float enemyHealth = 100;

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

    public void DamageTurret(float damage)
    {
        if(enemyHealth > 0)
        {
            enemyHealth -= damage;
            Debug.Log("Enemy health is now: " + enemyHealth);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Activation()
    {
        yield return new WaitForSeconds(timeUntilActiveTurret);
        turret.SetActive(true);

    }
}
