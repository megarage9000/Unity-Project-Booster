using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class BoosterProjectile : Projectile
{
   
    public override void OnDelete()
    {
        Debug.Log("Booster Projectile Deleted!");
        Destroy(gameObject);
    }

    public override void OnFire()
    {
        Debug.Log("Booster Projectile Fired!");
    }


    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        Debug.Log("Got " + tag);
        OnDelete();
    }

}
