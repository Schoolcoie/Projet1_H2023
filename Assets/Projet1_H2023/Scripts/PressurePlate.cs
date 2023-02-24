using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private List<GameObject> TriggerableObjects;
    private Action TriggerEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print($"Player stepped on Pressure Plate to trigger {TriggerableObjects}");
            //TriggerEvent += other.gameObject.GetComponent<>
        }
    }
}
