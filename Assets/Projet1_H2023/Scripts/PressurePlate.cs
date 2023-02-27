using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private List<GameObject> TriggerableObjects;
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
            foreach (var t in TriggerableObjects)
            {
                if(t.TryGetComponent(out ITriggerable obj))
                    obj.Trigger();
            }

            print($"Player stepped on Pressure Plate to trigger {TriggerableObjects}");
        }
    }
}
