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
        if (Input.GetKeyDown(KeyCode.F1))
        {
            showWindow = !showWindow;
        }
    }

    private void OnGUI()
    {
        if (showWindow)
        {
            godMode = GUILayout.Toggle(godMode, "GodMode");

            noClip = GUILayout.Toggle(noClip, "NoClip");

            


        }

    }



}
