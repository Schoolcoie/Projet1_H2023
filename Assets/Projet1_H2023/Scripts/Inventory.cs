using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Player m_Player;
    [SerializeField] private List<PassiveItem> m_PassiveItemInventory;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = FindObjectOfType<Player>();
        m_Player.OnItemPickup += UpdateInventory;
    }

    private void UpdateInventory(PassiveItem item)
    {
        m_PassiveItemInventory.Add(item);
    }

    public int GetInventoryCount()
    {
        return m_PassiveItemInventory.Count;
    }
}
