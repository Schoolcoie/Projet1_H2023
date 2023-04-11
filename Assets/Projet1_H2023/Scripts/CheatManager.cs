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
    private bool ghostBullets;
    private float playerSpeed = 5.0f;
    private Rect winRect = new Rect(10, 10, 200, 200);
    public bool InGodMode => godMode; //private getter
    public bool IsNoClipping => noClip;
    public bool BulletsIgnoreEnvironment => ghostBullets;
    public float PlayerSpeed => playerSpeed;

    private int EnemyTypeSelected = 0;
    private string[] EnemyTypeNames = { "Basic", "Uzi", "Sniper", "Shotgun" };
    [SerializeField] private Attack[] EnemyTypes;
    [SerializeField] private Weapon EnemyWeapon;

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
        EnemyPrefab.GetComponent<Enemy>().enemyAmmo = EnemyTypes[EnemyTypeSelected];
        EnemyPrefab.GetComponent<Enemy>().enemyWeapon = EnemyWeapon;

#if DEBUG
        if (Input.GetKeyDown(KeyCode.F1))
        {
            showWindow = !showWindow;
        }
#endif

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
            winRect = GUI.Window(0, winRect, WindowFunction, "Cheat Window");

        }
    }

    void WindowFunction(int windowID)
    {
        godMode = GUILayout.Toggle(godMode, "GodMode");

        noClip = GUILayout.Toggle(noClip, "NoClip");

        ghostBullets = GUILayout.Toggle(ghostBullets, "GhostBullets");

        GUILayout.Label("Player Speed");
        playerSpeed = GUILayout.HorizontalSlider(playerSpeed, 5.0f, 15.0f);

        EnemyTypeSelected = GUILayout.SelectionGrid(EnemyTypeSelected, EnemyTypeNames, 2);
    }
}
