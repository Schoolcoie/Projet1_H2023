using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemGeneratorWindow : EditorWindow
{
    private static ItemGeneratorWindow m_Window = null;

    [MenuItem("Window/Bart/ItemGeneratorWindow")]
    public static void ShowItemGeneratorWindow()
    {
        if (m_Window == null)
        {
            m_Window = GetWindow<ItemGeneratorWindow>();
            m_Window.titleContent = new GUIContent("Random Item Generator");
        }
    }


    Dictionary<string, bool> m_ToggleDict = new Dictionary<string, bool>();

    private bool ToggleName;
    private bool ToggleDamage;
    private bool ToggleAttackSpeed;

    private void OnEnable()
    {
        m_ToggleDict.Add(nameof(ToggleName), ToggleName);
        m_ToggleDict.Add(nameof(ToggleDamage), ToggleDamage);
        m_ToggleDict.Add(nameof(ToggleAttackSpeed), ToggleAttackSpeed);
    }
    

    private void OnGUI()
    {
        Debug.Log("OnGui");

       foreach (KeyValuePair<string, bool> valuePair in m_ToggleDict)
        {
            m_ToggleDict[valuePair.Key] = GUILayout.Toggle(m_ToggleDict[valuePair.Key], valuePair.Key);
        }



        if (GUILayout.Button("Generate Item"))
        {
            PassiveItem so = ScriptableObject.CreateInstance<PassiveItem>();

            if (m_ToggleDict["ToggleName"])
            {
                so.Name = "Default";
            }    

            if (m_ToggleDict["ToggleDamage"])
            {
                so.DamageMultiplier = Mathf.Round(UnityEngine.Random.Range(0.1f, 2f) * 100) / 100;
            } 

            AssetDatabase.CreateAsset(so, "/Assets/Projet1_H2023/Scripts/ScriptableObjects/PassiveItems");
        }


    }
}
