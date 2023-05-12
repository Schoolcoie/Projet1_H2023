using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    private Player m_Player;
    [SerializeField] private Image m_InventorySlot;
    private Transform m_Anchor;
    private Inventory m_Inventory;

    [SerializeField] private Text m_CurrentWeaponName;
    [SerializeField] private Text m_PrimaryWeaponName;
    [SerializeField] private Text m_SecondaryWeaponName;

    [SerializeField] private Text m_CurrentAmmoName;
    [SerializeField] private Text m_PrimaryAmmoName;
    [SerializeField] private Text m_SecondaryAmmoName;

    private void Awake()
    {
        m_Player = FindObjectOfType<Player>();
        m_Player.OnWeaponUpdate += UpdateWeaponUI;
        m_Player.OnAmmoUpdate += UpdateAmmoUI;
    }

    private void Start()
    {
        m_Player.OnItemPickup += UpdateInventoryUI;
        m_Anchor = transform.GetChild(0);
        m_Inventory = FindObjectOfType<Inventory>();

        m_Player.OnWeaponUpdate += UpdateWeaponUI;
        m_Player.OnAmmoUpdate += UpdateAmmoUI;
    }

    private void UpdateInventoryUI(PassiveItem item)
    {
        Image image = Instantiate(m_InventorySlot);
        image.transform.localPosition = new Vector3(m_Anchor.localPosition.x + (image.rectTransform.rect.width * m_Inventory.GetInventoryCount()), m_Anchor.localPosition.y, m_Anchor.localPosition.z);
        image.transform.SetParent(transform, false);
        image.sprite = Sprite.Create(item.Icon, new Rect(0, 0, item.Icon.width, item.Icon.height), new Vector2(0.5f, 0.5f));
    }

    private void UpdateWeaponUI(Weapon currentWeapon, Weapon primaryWeapon, Weapon secondaryWeapon)
    {
        m_CurrentWeaponName.text = "Current Weapon: " + currentWeapon.name;
        m_PrimaryWeaponName.text = "Weapon 1: " + primaryWeapon.name;
        m_SecondaryWeaponName.text = "Weapon 2: " + secondaryWeapon.name;


    }

    private void UpdateAmmoUI(Attack currentAmmo, Attack primaryAmmo, Attack secondaryAmmo)
    {
        m_CurrentAmmoName.text = "Current Ammo: " + currentAmmo.name;
        m_PrimaryAmmoName.text = "Ammo 1: " + primaryAmmo.name;
        m_SecondaryAmmoName.text = "Ammo 2: " + secondaryAmmo.name;
    }
}
