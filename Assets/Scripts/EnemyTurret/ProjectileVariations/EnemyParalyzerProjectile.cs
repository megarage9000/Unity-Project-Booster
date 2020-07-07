using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParalyzerProjectile : BaseEnemyProjectile
{


    public override void Awake()
    {
        base.Awake();
    }
    public override void AffectBoosterShip(GameObject boosterShip)
    {
        Rocket boosterScript = boosterShip.GetComponent<Rocket>();
        boosterScript.ExecuteParalysis();

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
