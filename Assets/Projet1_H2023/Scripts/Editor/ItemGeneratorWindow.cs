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

    private void OnEnable()
    {
       foreach (var field in typeof(PassiveItem).GetFields())
       {
            m_ToggleStateNames.Add(field.Name);
            m_ToggleStates.Add(false); 
       }
    }
    

    private void OnGUI()
    {
        Debug.Log("OnGui");

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

                    Debug.Log(temp.GetValue(so));
                }
               
            }
            //DestroyImmediate(so);
            AssetDatabase.CreateAsset(so, "Assets/Projet1_H2023/Scripts/ScriptableObjects/PassiveItems/Default.asset");
            AssetDatabase.SaveAssets();
        }


    }

    private object Randomize(Type type)
    {
        if (Type.Equals(type, typeof(string)))
        {
            Debug.Log("Its a string!");
            return "Gaming";
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

        Debug.Log(type);

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
