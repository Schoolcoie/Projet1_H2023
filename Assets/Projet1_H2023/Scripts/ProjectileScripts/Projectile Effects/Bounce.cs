using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : ProjectileEffect
{
    int BounceAmount = 2;

    public Bounce(Projectile projectile) : base(projectile)
    {
    }

    public override void OnEnemyCollisionEnter()
    {
    }

    public override void OnProjectileEnd()
    {
    }

    public override void OnStartOverride()
    {
    }

    public override void OnUpdate()
    {
    }

    public override void OnWallCollisionEnter()
    {
        BounceAmount--;
        Debug.Log("Bounce");

        if (BounceAmount == 0)
        {
            TargetProjectile.OnDestroy();
        }
        else
        {
            TargetProjectile.transform.forward = TargetProjectile.transform.forward * -1;
        }
    }

}
