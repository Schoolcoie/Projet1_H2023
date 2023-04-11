using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    public int Ammo;
    public int AttackCount;
    public float DelayBetweenAttacks;
    public float ReloadTime;
    public Attack DefaultAttackType;
    public Color Color;

    //public AttackPattern
}

