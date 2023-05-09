using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyStateMachine : EnemyStateMachine
{
    private Vector3 m_AttackDirection;
    public Vector3 attackdirection => m_AttackDirection;
    [SerializeField] private Projectile m_BulletPrefab;

    private bool m_OnCooldown = false;
    public bool oncooldown => m_OnCooldown;

    public Attack enemyAmmo;
    public Weapon enemyWeapon;

    public RangedEnemyStateMachine()
    {
        m_MaxHealth = 5;
        m_ContactDamage = 1;
    }

    // Update is called once per frame

    public void InstanciateBullet()
    {
        m_AttackDirection = m_Player.GetPlayerPosition - transform.position;
        Quaternion BulletRotation = Quaternion.LookRotation(new Vector3(m_AttackDirection.x, 0, m_AttackDirection.z), Vector3.up);

        for (int i = 0; i < enemyWeapon.AttackCount; i++)
        {
            Projectile bullet = Instantiate(m_BulletPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), BulletRotation);
            bullet.AttackProperties = enemyAmmo;
            bullet.AttackProperties.IsFriendly = false;
            bullet.Init();

            if (i == m_BulletPrefab.AttackProperties.ProjectileCount - 1)
            {
                StartCoroutine(CooldownRoutine(bullet.GetCooldown * 2));
            }
        }
    }

    private IEnumerator CooldownRoutine(float cooldown)
    {
        m_OnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        m_OnCooldown = false;
    }
}
