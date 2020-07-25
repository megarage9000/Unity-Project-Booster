using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class in which would modify the Boostership 
public abstract class BaseEnemyProjectile : Projectile
{
    private const string PLAYER_TAG = "Player";
    public abstract void AffectBoosterShip(GameObject boosterShip);

    private void OnCollisionEnter(Collision collision)
    {
        var gameObject = collision.gameObject;
        var tag = gameObject.tag;
        if (tag.Equals(PLAYER_TAG))
        {
            AffectBoosterShip(gameObject);
        }
        OnDelete();
    }

    public override void OnDelete()
    {
        Destroy(gameObject);
    }


}
