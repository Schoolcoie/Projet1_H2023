using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, ITriggerable
{
    private GameObject chestOpen;
    private GameObject chestClosed;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private LootTable lootTable;
    private List<ScriptableObject> lootList;
    private Transform itemSpawnLocation;

    private void Start()
    {
        itemSpawnLocation = gameObject.transform.GetChild(2);
        chestOpen = gameObject.transform.GetChild(0).gameObject;
        chestClosed = gameObject.transform.GetChild(1).gameObject;
    }

    public void Trigger()
    {
        if (chestOpen.activeSelf == false)
        {
            chestOpen.SetActive(true);
            chestClosed.SetActive(false);

            lootList = LootManager.Instance.GenerateLoot(lootTable);

            for (int x = 0; x < lootList.Count; x++)
            {
                GameObject m_item = Instantiate(itemPrefab, itemSpawnLocation);
                m_item.GetComponent<Item>().item = lootList[x];
            }
        }
    }
}
