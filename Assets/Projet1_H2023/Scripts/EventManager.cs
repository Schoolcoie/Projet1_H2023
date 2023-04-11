using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public static EventManager Instance;


    private static EventManager GetInstance()
    {
        if (!Instance)
        {
            Instance = FindObjectOfType<EventManager>();

            if (!Instance)
            {
                Debug.LogError("No EventManager found in scene");
            }
            else
            {
                //initialize dictionary
            }
        }

        return Instance;
    }


    private void StartListening()
    {

    }

    private void StopListening()
    {

    }

    private void TriggerEvent()
    {

    }
}
