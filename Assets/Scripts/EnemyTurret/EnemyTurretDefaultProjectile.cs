using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretDefaultProjectile : BaseEnemyProjectile
{

    public override void AffectBoosterShip(GameObject boosterShip)
    {
        Rocket boosterScript = boosterShip.GetComponent<Rocket>();
        boosterScript.ExecuteDeath();
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
}
