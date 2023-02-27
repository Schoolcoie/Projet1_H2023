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
    public bool IsFriendly; //to replace with prefab variant
    public int Spread;
    public int ProjectileCount; //to replace with weapon scriptable object
    public Color Color;
}
