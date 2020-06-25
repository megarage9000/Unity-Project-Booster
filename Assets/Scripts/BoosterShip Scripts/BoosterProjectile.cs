using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class BoosterProjectile : Projectile
{

    [SerializeField] float boosterProjectileSpeed = 100f;
    [SerializeField] float boosterProjectileRange = 2f;
   
    public override void OnDelete()
    {
        Debug.Log("Booster Projectile Deleted!");
        Destroy(gameObject);
    }

    public override void OnFire()
    {
        Debug.Log("Booster Projectile Fired!");
    }

    public override void SetupProjectile()
    {
        SetMaxProjectileRange(boosterProjectileRange);
        SetProjectileSpeed(boosterProjectileSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        Debug.Log("Got " + tag);
        OnDelete();
    }

}
