using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSize : ProjectileEffect
{

    public RandomSize(Projectile projectile) : base(projectile)
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
        float multiplier = Random.Range(0.5f, 2.2f);
        TargetProjectile.transform.localScale *= multiplier;
        TargetProjectile.Damage *= multiplier;
    }

    public override void OnUpdate()
    {
    }

    public override void OnWallCollisionEnter()
    {
    }
}
