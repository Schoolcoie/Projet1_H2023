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
    private float ProjectileLifespan;
    private bool IsFriendly;

    private Action<float> DealDamage;

    private void Awake()
    {
        Damage = AttackProperties.BaseDamage;
        Speed = AttackProperties.BaseTravelSpeed;
        Range = AttackProperties.BaseRange;
        Cooldown = AttackProperties.BaseAttackSpeed;
        ProjectileLifespan = AttackProperties.BaseLifeSpan;
        IsFriendly = AttackProperties.IsFriendly;
        GetComponent<MeshRenderer>().material.color = AttackProperties.Color;   
    }
    void Start()
    {
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

                DealDamage += other.gameObject.GetComponent<Enemy>().ApplyDamage;
                DealDamage(Damage);
                DealDamage -= other.gameObject.GetComponent<Enemy>().ApplyDamage;

                print($"Bullet dealt {Damage} to {other.gameObject.name}");

                Destroy(gameObject);
            }
        }
        else
        {

            if (other.gameObject.CompareTag("Player"))
            {
                print("Bullet collided with the Player");

                DealDamage += other.gameObject.GetComponent<Player>().ApplyDamage;
                DealDamage(Damage);
                DealDamage -= other.gameObject.GetComponent<Player>().ApplyDamage;

                //Apply invincibility frames

                print($"Bullet dealt {Damage} to {other.gameObject.name}");

                Destroy(gameObject);
            }
        }

        if (other.gameObject.CompareTag("WorldObject"))
        {
            //If object is breakable, Action to deal damage to object

            print("Bullet collided with world object");
            Destroy(gameObject);
        }
    }

}
