using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Attack AttackProperties; //need to turn this into an action

    private float Damage;
    public float GetDamage => Damage;
    private float Speed;
    private float Range;

    private float Cooldown;
    public float GetCooldown => Cooldown;
    public float Spread;
    private float ProjectileLifespan;
    private bool IsFriendly;
    public bool IsGhostly;



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
    }

    void Update()
    {   
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(ProjectileLifespan);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsFriendly)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                print("Bullet collided with an Enemy");

                other.gameObject.GetComponent<Enemy>().ApplyDamage(Damage);


                print($"Bullet dealt {Damage} to {other.gameObject.name}");

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
                Destroy(gameObject);
            }
        }

    }

}
