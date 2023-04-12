using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pierce : ProjectileEffect
{
    int PierceAmount = 2;

    public Pierce(Projectile projectile) : base(projectile)
    {
    }

    public override void OnEnemyCollisionEnter()
    {
        PierceAmount--;
        Debug.Log("Pierce");

        if (PierceAmount == 0)
        {
            TargetProjectile.OnDestroy();
        }
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
    }

}
