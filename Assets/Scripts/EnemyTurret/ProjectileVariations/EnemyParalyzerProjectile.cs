using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParalyzerProjectile : BaseEnemyProjectile
{
    [SerializeField] float paralyzeDuration = 1f;
    public override void AffectBoosterShip(GameObject boosterShip)
    {
        Rocket boosterScript = boosterShip.GetComponent<Rocket>();
        boosterScript.ExecuteParalysis();
    }

    
}
