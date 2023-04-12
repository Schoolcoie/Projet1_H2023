using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zigzag : ProjectileEffect
{
    float elapsed;

    public Zigzag(Projectile projectile) : base(projectile)
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
        elapsed += Time.deltaTime * 5;

        if (Mathf.Floor(elapsed) % 2 == 0)
        {
            TargetProjectile.transform.Translate(Vector3.right * Time.deltaTime * 10);
        }
        else
        {
            TargetProjectile.transform.Translate(Vector3.left * Time.deltaTime * 10);
        }
    }

    public override void OnWallCollisionEnter()
    {
    }

}
