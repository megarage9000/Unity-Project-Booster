using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class in which would modify the Boostership 
public abstract class BaseEnemyProjectile : Projectile
{
    public static readonly string PLAYER_TAG = "Player";
    public abstract void AffectBoosterShip(GameObject boosterShip);
    private Rigidbody rigidbody;

    public override void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody>();
    }
    public string GetPlayerString()
    {
        return PLAYER_TAG;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        var gameObject = collision.gameObject;
        var tag = gameObject.tag;
        if (tag.Equals(GetPlayerString()))
        {
            AffectBoosterShip(gameObject);
        }
        OnDelete();
        
        
        
    }


}
