using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{

    //turn into singleton
    [SerializeField] private Chest chest;
    [SerializeField] private LootTable table;
    private void Awake()
    {
        chest.onchestOpen += GenerateLoot;
    }

    private void GenerateLoot(Item item)
    {
        Debug.Log("Chest Opened");
        if (item.AttackProperties == null)
        {
            item.AttackProperties = table.Table[Random.Range(0, table.Table.Count)];
        }
        chest.onchestOpen -= GenerateLoot;
    }
}
