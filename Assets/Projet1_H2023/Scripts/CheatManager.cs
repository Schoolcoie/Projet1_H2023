using System;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    //Singleton
    private static CheatManager instance;

    public static CheatManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("CheatManager TP");
                go.AddComponent<CheatManager>();
            }

            return instance;
        }
    }

    //CheatManager
    private bool showWindow = false;
    private bool godMode;
    private bool noClip;
    public bool InGodMode => godMode; //private getter
    public bool IsNoClipping => noClip;

    private int EnemyTypeSelected = 0;
    private string[] EnemyTypeNames = { "Basic", "Uzi", "Sniper", "Shotgun" };
    [SerializeField] private Attack[] EnemyTypes;

    [SerializeField] private GameObject EnemyPrefab;

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

    private void Update()
    {
        EnemyPrefab.GetComponent<Enemy>().enemyAttack = EnemyTypes[EnemyTypeSelected];

        if (Input.GetKeyDown(KeyCode.F1))
        {
            showWindow = !showWindow;
        }

        if (showWindow)
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                Vector3 dir = FindObjectOfType<Player>().GetComponent<Player>().GetAttackDirection + FindObjectOfType<Player>().GetComponent<Player>().GetPlayerPosition;
                dir = new Vector3(dir.x, dir.y - 1, dir.z);

                Instantiate(EnemyPrefab, dir, Quaternion.identity);
            }
        }
       
    }

    private void OnGUI()
    {
        if (showWindow)
        {
            godMode = GUILayout.Toggle(godMode, "GodMode");

            noClip = GUILayout.Toggle(noClip, "NoClip");

            EnemyTypeSelected = GUILayout.SelectionGrid(EnemyTypeSelected, EnemyTypeNames, 2);
        }
    }
}
