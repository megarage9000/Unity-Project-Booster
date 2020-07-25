using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretScript : MonoBehaviour
{
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
