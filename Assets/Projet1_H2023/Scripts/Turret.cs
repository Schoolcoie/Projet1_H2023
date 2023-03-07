using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, ITriggerable
{
    [SerializeField] private Projectile BulletPrefab;
    [SerializeField] private GameObject Target;
    private Quaternion IdleRotation;
    private bool IsActive = false;
    private bool OnCooldown = false;
    public Attack enemyAttack;
    private float turretRange = 10.0f;

    public void Trigger()
    {
        IsActive = !IsActive;
    }

    // Start is called before the first frame update
    void Start()
    {
        IdleRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive)
        {
            if ((transform.position - Target.transform.position).magnitude < turretRange)
            {
                gameObject.transform.LookAt(new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z));
            }
            else
            {
                transform.rotation = IdleRotation;
            }

            if (!OnCooldown)
            {

                for (int i = 0; i < BulletPrefab.AttackProperties.ProjectileCount; i++)
                {
                    Projectile bullet = Instantiate(BulletPrefab, transform.Find("TurretBarrelExit").position, transform.rotation);
                    bullet.AttackProperties = enemyAttack;
                    bullet.AttackProperties.IsFriendly = true;
                    bullet.Init();

                    if (i == BulletPrefab.AttackProperties.ProjectileCount - 1)
                    {
                        StartCoroutine(CooldownRoutine(bullet.GetCooldown * 2));
                    }
                }
            }
        }
    }

    private IEnumerator CooldownRoutine(float cooldown)
    {
        OnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        OnCooldown = false;
    }



}
