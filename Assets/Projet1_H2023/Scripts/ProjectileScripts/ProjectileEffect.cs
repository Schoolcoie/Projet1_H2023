using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileEffect
{
    protected Projectile TargetProjectile;

    public ProjectileEffect(Projectile projectile)
    {
        TargetProjectile = projectile;
    }


    public abstract void OnWallCollisionEnter();
    public abstract void OnEnemyCollisionEnter();
    public abstract void OnProjectileEnd();
    public abstract void OnStartOverride();
    public abstract void OnUpdate();
}
