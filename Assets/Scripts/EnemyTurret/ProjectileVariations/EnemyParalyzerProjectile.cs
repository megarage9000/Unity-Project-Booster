using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParalyzerProjectile : BaseEnemyProjectile
{
    public override void AffectBoosterShip(GameObject boosterShip)
    {
        Debug.Log("Initializing paralyze effect");
        Rocket boosterScript = boosterShip.GetComponent<Rocket>();
        boosterScript.ExecuteParalysis();

    }

    public override void OnFire()
    {
        Debug.Log("Enemy Projectile Fired!");
    }

}
