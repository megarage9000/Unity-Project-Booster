using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretScript : MonoBehaviour
{
    const string PLAYER_PROJECTILE_TAG = "Player Projectile";
    private int enemyHealth = 100;

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
            BoosterProjectile boosterProjectile = collision.gameObject.GetComponent<BoosterProjectile>();
            damageTurret(boosterProjectile.GetDamage());
        }
    }

    private void damageTurret(int damage)
    {
        if(enemyHealth > 0)
        {
            enemyHealth -= damage;
        }
        else
        {
            Debug.Log("Enemy Turret dead");
            Destroy(gameObject);
        }
    }

    IEnumerator Activation()
    {
        yield return new WaitForSeconds(timeUntilActiveTurret);
        turret.SetActive(true);

    }
}
