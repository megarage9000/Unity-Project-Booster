using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageProjectile : BaseEnemyProjectile
{
    public override void AffectBoosterShip(GameObject boosterShip)
    {
        Rocket boosterScript = boosterShip.GetComponent<Rocket>();
        boosterScript.ExecuteDeath();
    }

    public override void OnFire()
    {
        Debug.Log("Enemy Projectile Fired!");
    }

}
