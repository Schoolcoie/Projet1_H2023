using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody PlayerBody;
    [SerializeField] private float PlayerSpeed = 5.0f;
    private float MaxHealth = 5;
    private float CurrentHealth;
    private Attack currentWeapon;
    [SerializeField] private Attack primaryWeapon;
    [SerializeField] private Attack secondaryWeapon;
    private float HalfScreenWidth = Screen.width / 2;
    private float HalfScreenHeight = Screen.height / 2;
    private bool IsDead;

    private Vector3 playerPosition;
    public Vector3 GetPlayerPosition => playerPosition;

    private Item hoveredItem;

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private Projectile BulletPrefab;

    int PlaneLayer = 1 << 3;

    private bool OnCooldown = false;

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
        BulletPrefab.AttackProperties = currentWeapon;
    }

    void Update()
    {
        print(Camera.main.transform.right);
        playerPosition = transform.position;

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
                }
                else if (currentWeapon == secondaryWeapon && primaryWeapon != null)
                {
                    currentWeapon = primaryWeapon;
                }

            }

            if (hoveredItem != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (currentWeapon == primaryWeapon)
                    {
                        currentWeapon = hoveredItem.AttackProperties;
                        hoveredItem.AttackProperties = primaryWeapon;
                        primaryWeapon = currentWeapon;
                    }
                    else
                    {
                        currentWeapon = hoveredItem.AttackProperties;
                        hoveredItem.AttackProperties = secondaryWeapon;
                        secondaryWeapon = currentWeapon;
                        
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

            print(OnCooldown);

            if (!OnCooldown)
            {
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    BulletPrefab.AttackProperties = currentWeapon;
                    BulletPrefab.AttackProperties.IsFriendly = true;

                    for (int i = 0; i < BulletPrefab.AttackProperties.ProjectileCount; i++)
                    {
                        Projectile bullet = Instantiate(BulletPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), BulletRotation);

                        if (i == BulletPrefab.AttackProperties.ProjectileCount - 1)
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
        float DashTimer = 0;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            transform.position = Vector2.Lerp(transform.position, transform.position - Vector3.up, DashTimer);
        }
    }

    public void ApplyDamage(float Damage)
    {
        if (!CheatManager.Instance.InGodMode)
        {

            CurrentHealth -= Damage;

            //update the UI
            testhealth.transform.localScale = new Vector3(CurrentHealth / MaxHealth, testhealth.transform.localScale.y, testhealth.transform.localScale.z);

            if (CurrentHealth <= 0)
            {
                IsDead = true;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            print($"Detected collision with item {other.name}");
            hoveredItem = other.gameObject.GetComponent<Item>(); //temporarily only weapons
            //communicate with the UI to show prompt to pick it up
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            print($"Left collision with item {other.name}");
            hoveredItem = null;
            //communicate with the UI to hide prompt to pick it up
        }
    }
}
