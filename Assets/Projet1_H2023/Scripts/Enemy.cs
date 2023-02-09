using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float MaxHealth = 5;
    private float CurrentHealth = 5; 
    private float Damage = 1;

    [SerializeField]
    private GameObject testhealth;

    private Action<float> DamageAction;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float Damage)
    {
        CurrentHealth -= Damage;

        testhealth.transform.localScale = new Vector3(CurrentHealth / MaxHealth, testhealth.transform.localScale.y, testhealth.transform.localScale.z);

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
