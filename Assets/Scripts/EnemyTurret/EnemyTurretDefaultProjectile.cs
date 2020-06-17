using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretDefaultProjectile : Projectile
{
    [SerializeField] float enemyDefaultProjectileSpeed;
    [SerializeField] float enemyDefaultProjectileRange;
    // Start is called before the first frame update
    void Start()
    {
        SetupProjectile();
    }
    public override void OnDelete()
    {
        Debug.Log("Enemy Projectiled Deleted!");
        Destroy(gameObject);
    }

    public override void OnFire()
    {
        Debug.Log("Enemy Projectile Fired!");
    }

    public override void SetupProjectile()
    {
        SetProjectileRange(enemyDefaultProjectileRange);
        SetProjectileSpeed(enemyDefaultProjectileSpeed);
        Fire();
    }

 


}
