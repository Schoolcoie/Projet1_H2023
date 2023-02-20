using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Attack: ScriptableObject
{
    public string Name = "Default";
    public int BaseDamage;
    public float BaseTravelSpeed;
    public float BaseAttackSpeed;
    public float BaseRange;
    public float BaseLifeSpan;
    public bool IsFriendly;
    public Color Color;
}
