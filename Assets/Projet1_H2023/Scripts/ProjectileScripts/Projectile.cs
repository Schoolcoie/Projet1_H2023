using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Attack AttackProperties; //need to turn this into an action

    public float Damage;
    public float GetDamage => Damage;
    private float Speed;
    private float Range;

    private float Cooldown;
    public float GetCooldown => Cooldown;
    public float Spread;
    private float ProjectileLifespan;
    private bool IsFriendly;
    public bool IsGhostly;

    private bool SpecialWallCollision;
    private bool SpecialEnemyCollision;

    private List<ProjectileEffect> EffectsList = new List<ProjectileEffect>();



    public void Init(float damagemultiplier = 1, float attackspeedmultiplier = 1, float accuracymultiplier = 1, float rangemultiplier = 1, float projectilespeedmultiplier = 1, float projectilesizemultiplier = 1)
    {
        Damage = AttackProperties.BaseDamage * damagemultiplier;
        Speed = AttackProperties.BaseTravelSpeed * projectilespeedmultiplier;
        Range = AttackProperties.BaseRange;
        Cooldown = AttackProperties.BaseAttackSpeed / attackspeedmultiplier;
        Spread = AttackProperties.Spread / accuracymultiplier;
        ProjectileLifespan = AttackProperties.BaseLifeSpan * rangemultiplier;
        transform.localScale = transform.localScale * projectilesizemultiplier;
        IsFriendly = AttackProperties.IsFriendly;
        GetComponent<MeshRenderer>().material.color = AttackProperties.Color;

    }

    void Start()
    {
        Speed += UnityEngine.Random.Range(0, Spread / 10);
        transform.rotation *= Quaternion.AngleAxis(UnityEngine.Random.Range(-Spread, Spread), new Vector3(0, 1, 0));
        StartCoroutine(DeathCoroutine());

        foreach (ProjectileEffect eff in EffectsList)
        {
            eff.OnStartOverride();
        }
    }

    void Update()
    {   
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);

        foreach (ProjectileEffect eff in EffectsList)
        {
            eff.OnUpdate();
        }
    }

    public void AddProjectileEffect(PassiveItem.EProjectileEffects effect)
    {
        switch (effect)
        {
            case PassiveItem.EProjectileEffects.Bomb:
                EffectsList.Add(new Bomb(this));
                break;
            case PassiveItem.EProjectileEffects.Pierce:
                EffectsList.Add(new Pierce(this));
                SpecialEnemyCollision = true;
                break;
            case PassiveItem.EProjectileEffects.Bounce:
                EffectsList.Add(new Bounce(this));
                SpecialWallCollision = true;
                break;
            case PassiveItem.EProjectileEffects.Zigzag:
                EffectsList.Add(new Zigzag(this));
                break;
            case PassiveItem.EProjectileEffects.RandomSize:
                EffectsList.Add(new RandomSize(this));
                break;
        }
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(ProjectileLifespan);

        foreach (ProjectileEffect eff in EffectsList)
        {
            eff.OnProjectileEnd();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsFriendly)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                print("Bullet collided with an Enemy");

                other.gameObject.GetComponent<EnemyStateMachine>().ApplyDamage(Damage);


                print($"Bullet dealt {Damage} to {other.gameObject.name}");


                foreach (ProjectileEffect eff in EffectsList)
                {
                    eff.OnEnemyCollisionEnter();
                }

                if (SpecialEnemyCollision == false)
                    Destroy(gameObject);
            }
        }
        else
        {

            if (other.gameObject.CompareTag("Player"))
            {
                print("Bullet collided with the Player");

                other.gameObject.GetComponent<Player>().ApplyDamage(Damage);

                //Apply invincibility frames

                print($"Bullet dealt {Damage} to {other.gameObject.name}");


                Destroy(gameObject);
            }
        }

        if (IsGhostly == false)
        {
            if (other.gameObject.CompareTag("WorldObject"))
            {
                //If object is breakable, Action to deal damage to object

                print("Bullet collided with world object");

                foreach (ProjectileEffect eff in EffectsList)
                {
                    eff.OnWallCollisionEnter();
                }

                if(SpecialWallCollision == false)
                    Destroy(gameObject);
            }
        }
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }

}
