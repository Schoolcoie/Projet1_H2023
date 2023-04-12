using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : ProjectileEffect
{
    public Bomb(Projectile projectile): base(projectile)
    {
    }

    public override void OnEnemyCollisionEnter()
    {
        Debug.Log("Boom");
    }

    public override void OnProjectileEnd()
    {
        Debug.Log("Boom");
    }

    public override void OnStartOverride()
    {
    }

    public override void OnUpdate()
    {
    }

    public override void OnWallCollisionEnter()
    {
        Debug.Log("Boom");
    }

}
