using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParalyzerProjectile : BaseEnemyProjectile
{
    public override void AffectBoosterShip(GameObject boosterShip)
    {
        Rocket boosterScript = boosterShip.GetComponent<Rocket>();
        boosterScript.ExecuteParalysis();
    }
}
