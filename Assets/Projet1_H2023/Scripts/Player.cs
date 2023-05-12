using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody PlayerBody;

    //Base Stats

    [SerializeField] private float PlayerSpeed = 5.0f;
    private float MaxHealth = 5;
    private float CurrentHealth;

    //Stat modifiers

    [SerializeField] private float PlayerDamageMultiplier = 1.0f;
    [SerializeField] private float PlayerAttackSpeedMultiplier = 1.0f;
    [SerializeField] private float PlayerRangeMultiplier = 1.0f;
    [SerializeField] private float PlayerProjectileSpeedMultiplier = 1.0f;
    [SerializeField] private float PlayerAccuracyMultiplier = 1.0f;
    [SerializeField] private float PlayerProjectileSizeMultiplier = 1.0f;

    [SerializeField] private int PlayerProjectileModifier = 0;

    //Weapons

    [SerializeField] private Weapon primaryWeapon;
    [SerializeField] private Weapon secondaryWeapon;
    [SerializeField] private Weapon currentWeapon;

    public Action<Weapon, Weapon, Weapon> OnWeaponUpdate;

    //Ammos

    [SerializeField] private Attack primaryAmmo;
    [SerializeField] private Attack secondaryAmmo;
    [SerializeField] private Attack currentAmmo;

    public Action<Attack, Attack, Attack> OnAmmoUpdate;

    //Passive Items and Inventory

    private List<PassiveItem> currentItems = new List<PassiveItem>();
    public Action<PassiveItem> OnItemPickup;

    private float HalfScreenWidth = Screen.width / 2;
    private float HalfScreenHeight = Screen.height / 2;
    private bool IsDead;

    // Misc 
    private Vector3 playerPosition;
    public Vector3 GetPlayerPosition => playerPosition;

    private Item hoveredItem;

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private Projectile BulletPrefab;

    int PlaneLayer = 1 << 3;

    private bool IsInvulnerable = false;
    private bool OnCooldown = false;

    private bool CanDash = true;
    private bool IsDashing = false;

    private Vector3 AttackDirection;
    public Vector3 GetAttackDirection => AttackDirection;

    [SerializeField]
    private GameObject testhealth;

    //Cursor Settings
    [SerializeField] private Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;


    void Start()
    {
        //Cursor init
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        //Cursor.lockState = CursorLockMode.Confined;
        PlayerBody = GetComponent<Rigidbody>();
        //Events Init
        //Player Init
        CurrentHealth = MaxHealth;
        currentWeapon = primaryWeapon;
        currentAmmo = primaryAmmo;

        OnWeaponUpdate?.Invoke(currentWeapon, primaryWeapon, secondaryWeapon);
        OnAmmoUpdate?.Invoke(currentAmmo, primaryAmmo, secondaryAmmo);

        OnItemPickup += CalculateStats;

        //Initialize stats
    }

    void Update()
    {
        playerPosition = transform.position;
        //PlayerSpeed = CheatManager.Instance.PlayerSpeed;

        if (CheatManager.Instance.IsNoClipping)
        {
            GetComponent<Collider>().isTrigger = true;
        }
        else
        {
            GetComponent<Collider>().isTrigger = false;
        }

        if (!IsDead)
        {
            Movement();

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (currentWeapon == primaryWeapon && secondaryWeapon != null)
                {
                    currentWeapon = secondaryWeapon;
                    currentAmmo = secondaryAmmo;

                    OnWeaponUpdate?.Invoke(currentWeapon, primaryWeapon, secondaryWeapon);
                    OnAmmoUpdate?.Invoke(currentAmmo, primaryAmmo, secondaryAmmo);
                }
                else if (currentWeapon == secondaryWeapon && primaryWeapon != null)
                {
                    currentWeapon = primaryWeapon;
                    currentAmmo = primaryAmmo;

                    OnWeaponUpdate?.Invoke(currentWeapon, primaryWeapon, secondaryWeapon);
                    OnAmmoUpdate?.Invoke(currentAmmo, primaryAmmo, secondaryAmmo);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentAmmo = primaryAmmo;

                OnWeaponUpdate?.Invoke(currentWeapon, primaryWeapon, secondaryWeapon);
                OnAmmoUpdate?.Invoke(currentAmmo, primaryAmmo, secondaryAmmo);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentAmmo = secondaryAmmo;

                OnWeaponUpdate?.Invoke(currentWeapon, primaryWeapon, secondaryWeapon);
                OnAmmoUpdate?.Invoke(currentAmmo, primaryAmmo, secondaryAmmo);
            }

            if (hoveredItem != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hoveredItem.item is Attack)
                    {
                        Attack temp = (Attack)hoveredItem.item;

                        if (currentAmmo == primaryAmmo)
                        {
                            currentAmmo = temp;
                            hoveredItem.item = primaryAmmo;
                            primaryAmmo = temp;
                            OnAmmoUpdate?.Invoke(currentAmmo, primaryAmmo, secondaryAmmo);
                        }
                        else
                        {
                            currentAmmo = temp;
                            hoveredItem.item = secondaryAmmo;
                            secondaryAmmo = temp;
                            OnAmmoUpdate?.Invoke(currentAmmo, primaryAmmo, secondaryAmmo);
                        }
                    }

                    if (hoveredItem.item is Weapon)
                    {
                        Weapon temp = (Weapon)hoveredItem.item;
                        if (currentWeapon == primaryWeapon)
                        {
                            currentWeapon = temp;
                            hoveredItem.item = primaryWeapon;
                            primaryWeapon = currentWeapon;
                            OnWeaponUpdate?.Invoke(currentWeapon, primaryWeapon, secondaryWeapon);
                        }
                        else
                        {
                            currentWeapon = temp;
                            hoveredItem.item = secondaryWeapon;
                            secondaryWeapon = currentWeapon;
                            OnWeaponUpdate?.Invoke(currentWeapon, primaryWeapon, secondaryWeapon);

                            if (secondaryAmmo == null)
                            {
                                secondaryAmmo = secondaryWeapon.DefaultAttackType;
                                OnAmmoUpdate?.Invoke(currentAmmo, primaryAmmo, secondaryAmmo);
                            }
                        }
                    }

                    if (hoveredItem.item is PassiveItem)
                    {
                        OnItemPickup?.Invoke((PassiveItem)hoveredItem.item);
                        currentItems.Add((PassiveItem)hoveredItem.item);
                        hoveredItem.item = null;
                    }
                }
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.white);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, PlaneLayer))
            {
                AttackDirection = hit.point - transform.position;
            }

            Quaternion BulletRotation = Quaternion.LookRotation(new Vector3(AttackDirection.x, 0, AttackDirection.z), Vector3.up);

            if (!OnCooldown && !IsDashing)
            {
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    for (int i = 0; i < currentWeapon.AttackCount + PlayerProjectileModifier; i++)
                    {
                        Projectile bullet = Instantiate(BulletPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), BulletRotation);
                        bullet.AttackProperties = currentAmmo;
                        bullet.AttackProperties.IsFriendly = true;

                        //Add projectile effects if applicable
                        foreach (PassiveItem item in currentItems)
                        {
                            if (item.Effects != null)
                            {
                                for (int j = 0; j < item.Effects.Count; j++)
                                {
                                    bullet.AddProjectileEffect(item.Effects[j]);
                                }
                            }
                        }

                        if (CheatManager.Instance.BulletsIgnoreEnvironment)
                        {
                            bullet.IsGhostly = true;
                        }

                        bullet.Init(PlayerDamageMultiplier, PlayerAttackSpeedMultiplier, PlayerAccuracyMultiplier, PlayerRangeMultiplier, PlayerProjectileSpeedMultiplier, PlayerProjectileSizeMultiplier);

                        if (i == currentWeapon.AttackCount - 1)
                        {
                            StartCoroutine(CooldownRoutine(bullet.GetCooldown));
                        }
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

    private IEnumerator InvulnerabilityRoutine(float invuln)
    {
        PlayerSpeed *= 1.5f;
        IsInvulnerable = true;
        StartCoroutine(InvulnerabilityBlink());
        yield return new WaitForSeconds(invuln);
        IsInvulnerable = false;
        PlayerSpeed /= 1.5f;
    }

    private IEnumerator InvulnerabilityBlink()
    {
        Color spritecolor = transform.GetChild(0).GetComponent<SpriteRenderer>().material.color;

        while (IsInvulnerable == true)
        {
            print("blinking");
            spritecolor = new Color(spritecolor.r, spritecolor.g, spritecolor.b, 0.5f);
            transform.GetChild(0).GetComponent<SpriteRenderer>().material.color = spritecolor;
            yield return new WaitForSeconds(0.075f);
            spritecolor = new Color(spritecolor.r, spritecolor.g, spritecolor.b, 1);
            transform.GetChild(0).GetComponent<SpriteRenderer>().material.color = spritecolor;
            yield return new WaitForSeconds(0.075f);
        }
    }

    private void Movement()
    {
        //Directional Movement
        float VerticalMovement = Input.GetAxis("Vertical");
        float HorizontalMovement = Input.GetAxis("Horizontal");
        PlayerBody.velocity = (new Vector3(0.7f, 0, 0.7f) * VerticalMovement * PlayerSpeed) + (Camera.main.transform.right * HorizontalMovement * PlayerSpeed);

        if (PlayerBody.velocity.x != 0 || PlayerBody.velocity.z != 0)
        {
            playerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            playerAnimator.SetBool("IsWalking", false);
        }


        //Dash

        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash)
        {
            StartCoroutine(InvulnerabilityRoutine(1));
            StartCoroutine(DashCooldownRoutine(3));
        }
    }

    private IEnumerator DashCooldownRoutine(float cooldown)
    {
        transform.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        CanDash = false;
        IsDashing = true;
        yield return new WaitForSeconds(cooldown / 3);
        IsDashing = false;
        transform.GetComponentInChildren<SpriteRenderer>().color = Color.cyan;
        yield return new WaitForSeconds((cooldown / 3) * 2);
        CanDash = true;
        transform.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    public void ApplyDamage(float Damage)
    {
        if (!CheatManager.Instance.InGodMode && IsInvulnerable == false)
        {
            CurrentHealth -= Damage;

            testhealth.transform.localScale = new Vector3(CurrentHealth / MaxHealth, testhealth.transform.localScale.y, testhealth.transform.localScale.z);

            if (CurrentHealth <= 0)
            {
                IsDead = true;
                SceneManager.LoadScene("Main Menu");
            }

            StartCoroutine(InvulnerabilityRoutine(1.5f));
        }
    }

    private void CalculateStats(PassiveItem item)
    {
        Debug.Log("Calculated stats");
        if (item.DamageMultiplier != 0)
        {
            PlayerDamageMultiplier *= item.DamageMultiplier;
            print("Changed Player Damage");
        }

        if (item.AttackSpeedMultiplier != 0)
        {
            PlayerAttackSpeedMultiplier *= item.AttackSpeedMultiplier;
            print("Changed Player Attack Speed");
        }

        if (item.SpeedMultiplier != 0)
        {
            PlayerSpeed *= item.SpeedMultiplier;

            print("Changed Player Speed");
        }

        if (item.HPMultiplier != 0)
        {
            float temp = MaxHealth;
            MaxHealth *= item.HPMultiplier;
            CurrentHealth += MaxHealth - temp;
            print("Changed Player Max Health");
        }

        if (item.AccuracyMultiplier != 0)
        {
            PlayerAccuracyMultiplier *= item.AccuracyMultiplier;

            print("Changed Player Spread");
        }

        if (item.ProjectileSpeedMultiplier != 0)
        {
            PlayerProjectileSpeedMultiplier *= item.ProjectileSpeedMultiplier;

            print("Changed Player Projectile Speed");
        }

        if (item.RangeMultiplier != 0)
        {
            PlayerRangeMultiplier *= item.RangeMultiplier;

            print("Changed Player Range");
        }

        if (item.ProjectileSizeMultiplier != 0)
        {
            PlayerProjectileSizeMultiplier *= item.ProjectileSizeMultiplier;

            print("Changed Player Projectile Size");
        }

        if (item.ExtraProjectileModifier != 0)
        {
            PlayerProjectileModifier += item.ExtraProjectileModifier;

            print("Changed Player Projectile Amount");
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Item"))
        {
            hoveredItem = other.gameObject.GetComponent<Item>();
            //communicate with the UI to show prompt to pick it up
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            hoveredItem = null;
            //communicate with the UI to hide prompt to pick it up
        }
    }
}
