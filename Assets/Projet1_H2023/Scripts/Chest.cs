using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, ITriggerable
{
    [SerializeField] private GameObject chestOpen;
    [SerializeField] private GameObject chestClosed;
    [SerializeField] private Item chestLoot;
    [SerializeField] private LootTable table;
    public Action<Item> onchestOpen;

    public void Trigger()
    {
        chestOpen.SetActive(true);
        chestClosed.SetActive(false);
        chestLoot.gameObject.SetActive(true);
        onchestOpen?.Invoke(chestLoot);
    }


}
