using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float MaxHealth = 5;
    private float CurrentHealth; 
    private float ContactDamage = 1;
    private bool OnCooldown = false;
    private Vector3 AttackDirection;
    [SerializeField] private Projectile BulletPrefab;

    [SerializeField]
    private GameObject testhealth;

    [SerializeField]
    private Attack enemyAttack;

    private Projectile instance;


    private Action<float> DamageAction;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        BulletPrefab.AttackProperties.IsFriendly = false;
    }

    // Update is called once per frame
    void Update()
    {
        AttackDirection = FindObjectOfType<Player>().GetPlayerPosition - transform.position;

        Quaternion BulletRotation = Quaternion.LookRotation(new Vector3(AttackDirection.x, 0, AttackDirection.z), Vector3.up);

        if (!OnCooldown)
        {
            BulletPrefab.AttackProperties = enemyAttack;
            BulletPrefab.AttackProperties.IsFriendly = false;
            Projectile bullet = Instantiate(BulletPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), BulletRotation);
            StartCoroutine(CooldownRoutine(bullet.GetCooldown));
        }
    }

    private IEnumerator CooldownRoutine(float cooldown)
    {
        OnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        OnCooldown = false;
    }

    public void ApplyDamage(float Damage)
    {
        CurrentHealth -= Damage;

        testhealth.transform.localScale = new Vector3(CurrentHealth / MaxHealth, testhealth.transform.localScale.y, testhealth.transform.localScale.z);

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
