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

    private void Start()
    {
        m_Player = FindObjectOfType<Player>();
        m_Player.OnItemPickup += UpdateInventoryUI;
        m_Anchor = transform.GetChild(0);
        m_Inventory = FindObjectOfType<Inventory>();
    }

    private void UpdateInventoryUI(PassiveItem item)
    {
        Image image = Instantiate(m_InventorySlot);
        image.transform.localPosition = new Vector3(m_Anchor.localPosition.x + (image.rectTransform.rect.width * m_Inventory.GetInventoryCount()), m_Anchor.localPosition.y, m_Anchor.localPosition.z);
        image.transform.SetParent(transform, false);
        image.sprite = Sprite.Create(item.Icon, new Rect(0, 0, item.Icon.width, item.Icon.height), new Vector2(0.5f, 0.5f));
    }
}
