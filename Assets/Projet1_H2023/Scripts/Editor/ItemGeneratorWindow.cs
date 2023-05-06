using System;
using System.Collections.Generic;
using System.Reflection;
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

    List<string> m_ToggleStateNames = new List<string>();
    List<bool> m_ToggleStates = new List<bool>();
    string[] m_GeneratedItemGUIDs = new string[0];
    PassiveItem m_SelectedPassiveItem;
    string[] m_PassiveItemTextFields;
    int m_SelectedIndex = 0;
    string[] m_PassiveItemFolder = new string[1];
    string m_PassiveItemPath = "Assets/Projet1_H2023/Scripts/ScriptableObjects/PassiveItems";
    Vector2 m_ScrollPosition;

    private void OnEnable()
    {
       foreach (var field in typeof(PassiveItem).GetFields())
       {
            m_ToggleStateNames.Add(field.Name);
            m_ToggleStates.Add(false);
       }
        m_PassiveItemTextFields = new string[m_ToggleStates.Count];
        m_PassiveItemFolder[0] = m_PassiveItemPath;
        UpdateGeneratedAssets();
    }

    private void OnGUI()
    {
        for (int i = 0; i < m_ToggleStates.Count; i++)
        {
            m_ToggleStates[i] = GUILayout.Toggle(m_ToggleStates[i], m_ToggleStateNames[i]);
        }

        if (GUILayout.Button("Generate Item"))
        {
            PassiveItem so = ScriptableObject.CreateInstance<PassiveItem>();
            
            for (int i = 0; i < m_ToggleStates.Count; i++)
            {
                if (m_ToggleStates[i] == true)
                {
                    FieldInfo temp = typeof(PassiveItem).GetField(m_ToggleStateNames[i]);
                    temp.SetValue(so, Randomize(temp.FieldType));
                } 
            }
            AssetDatabase.CreateAsset(so, m_PassiveItemPath + $"/#{m_GeneratedItemGUIDs.Length} Random Item.asset");
            AssetDatabase.SaveAssets();
            UpdateGeneratedAssets();
        }

        GUILayout.Label("Randomly Generated Items: ");
        m_SelectedIndex = GUILayout.SelectionGrid(m_SelectedIndex, m_GeneratedItemGUIDs, 2);

        if (GUILayout.Button("Select Object") && m_GeneratedItemGUIDs.Length > 0)
        {
            m_SelectedPassiveItem = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(m_GeneratedItemGUIDs[m_SelectedIndex]), typeof(ScriptableObject)) as PassiveItem;

            for (int i = 0; i < m_ToggleStates.Count; i++)
            {
                FieldInfo temp = typeof(PassiveItem).GetField(m_ToggleStateNames[i]);

                if (temp.GetValue(m_SelectedPassiveItem) != null)
                m_PassiveItemTextFields[i] = temp.GetValue(m_SelectedPassiveItem).ToString();
            }
        }

        if (m_SelectedPassiveItem != null)
        {
            GUILayout.Label("Currently Selected: " + m_SelectedPassiveItem.name);

            m_ScrollPosition = GUILayout.BeginScrollView(m_ScrollPosition);

            for (int i = 1; i < m_ToggleStates.Count - 1; i++)
            {
                FieldInfo temp = typeof(PassiveItem).GetField(m_ToggleStateNames[i]);

                GUILayout.Label(m_ToggleStateNames[i]);

                m_PassiveItemTextFields[i] = GUILayout.TextField(m_PassiveItemTextFields[i]);

                if (Type.Equals(temp.FieldType, typeof(string)))
                {
                    temp.SetValue(m_SelectedPassiveItem, m_PassiveItemTextFields[i]);
                }
                else if (Type.Equals(temp.FieldType, typeof(float)))
                {
                    if (float.TryParse(m_PassiveItemTextFields[i], out float floattest))
                    {
                        temp.SetValue(m_SelectedPassiveItem, float.Parse(m_PassiveItemTextFields[i]));
                    }
                    else
                    {
                        GUI.color = Color.red;
                        GUILayout.Label("WARNING: INPUT TYPE INVALID");
                        GUI.color = Color.white;
                    }
                }
                else if (Type.Equals(temp.FieldType, typeof(int)))
                {
                    if (int.TryParse(m_PassiveItemTextFields[i], out int inttest))
                    {
                        temp.SetValue(m_SelectedPassiveItem, int.Parse(m_PassiveItemTextFields[i]));
                    }
                    else
                    {
                        GUI.color = Color.red;
                        GUILayout.Label("WARNING: INPUT TYPE INVALID");
                        GUI.color = Color.white;
                    }
                }
            }

            GUILayout.EndScrollView();

            if (GUILayout.Button("Add Selected Item to Passive Loot Table"))
            {
                string[] loottableassetfolder = new string[1];
                Debug.Log(loottableassetfolder);
                string loottableassetpath = "Assets/Projet1_H2023/Scripts/ScriptableObjects/Loot/Passive.asset";
                loottableassetfolder[0] = loottableassetpath;
                LootTable passiveloottable = AssetDatabase.LoadAssetAtPath(loottableassetpath, typeof(ScriptableObject)) as LootTable;
                
                if (!passiveloottable.Table.Contains(m_SelectedPassiveItem))
                {
                    passiveloottable.Table.Add(m_SelectedPassiveItem);
                }
            }

            if (GUILayout.Button("Delete Selected Item"))
            {
                string[] loottableassetfolder = new string[1];
                string loottableassetpath = "Assets/Projet1_H2023/Scripts/ScriptableObjects/Loot/Passive.asset";
                loottableassetfolder[0] = loottableassetpath;
                LootTable passiveloottable = AssetDatabase.LoadAssetAtPath(loottableassetpath, typeof(ScriptableObject)) as LootTable;
                passiveloottable.Table.Remove(m_SelectedPassiveItem);

                Debug.Log("Deleted " + m_SelectedPassiveItem.name);
                AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(m_GeneratedItemGUIDs[m_SelectedIndex]));
                UpdateGeneratedAssets();
            }
        }
    }

    private void UpdateGeneratedAssets()
    {
        m_GeneratedItemGUIDs = AssetDatabase.FindAssets("Random Item", m_PassiveItemFolder);
        AssetDatabase.SaveAssets();
    }

    private object Randomize(Type type)
    {
        if (Type.Equals(type, typeof(string)))
        {
            Debug.Log("Its a string!");
            return "Placeholder"; // Did not implement a way to randomize Name or Description sorry
        }

        if (Type.Equals(type, typeof(float)))
        {
            Debug.Log("Its a float!");
            return Mathf.Round(UnityEngine.Random.Range(0.1f, 2) * 100) / 100;
        }

        if (Type.Equals(type, typeof(int)))
        {
            Debug.Log("Its an integer!");
            return UnityEngine.Random.Range(1, 5);
        }

        if (type is ICollection<Enum>)
        {
            Debug.Log("Its an Enum!");
            int enumcount = Enum.GetValues(type).Length;
            string[] enumname = Enum.GetNames(type);
            var temp = Enum.Parse(type, enumname[UnityEngine.Random.Range(0, enumcount)]);
            Debug.Log(temp);

            return temp;
        }
        return null;
    }
}