using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{

    //Singleton
    private static LootManager instance;

    public static LootManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Loot Manager");
                go.AddComponent<LootManager>();
            }

            return instance;
        }
    }

    [SerializeField] private Chest chest;
    [SerializeField] private LootTable table;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        chest.onchestOpen += GenerateLoot;

    }

    private void GenerateLoot(Item loot)
    {
        if (loot.item == null)
        {
            loot.item = table.Table[Random.Range(0, table.Table.Count)];
            print($"Put {loot.item.name} in object");
        }
        chest.onchestOpen -= GenerateLoot;
    }
}
