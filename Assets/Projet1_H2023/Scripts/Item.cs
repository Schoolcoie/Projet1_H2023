using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public ScriptableObject item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (item is Attack && item)
        {
            Attack temp = (Attack)item;
            GetComponent<MeshRenderer>().material.color = temp.Color;

        }
        else if (item is Weapon)
        {
            Weapon temp = (Weapon)item;
            GetComponent<MeshRenderer>().material.color = temp.Color;
        }

        if (item == null)
        {
            //Destroy(gameObject);
        }
    }
}
