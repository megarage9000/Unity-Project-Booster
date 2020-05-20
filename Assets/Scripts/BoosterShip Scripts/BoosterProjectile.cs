using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterProjectile : Projectile
{

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
