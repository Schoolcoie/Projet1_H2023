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
    }

    public List<ScriptableObject> GenerateLoot()
    {
        List<ScriptableObject> m_LootList = new List<ScriptableObject>();

            m_LootList.Add(table.Table[Random.Range(0, table.Table.Count)]);
            print($"Put {m_LootList[0]} in object");

        return m_LootList; 
    }
}
