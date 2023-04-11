using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PassiveItems : ScriptableObject
{
    public Texture2D Icon;
    public string Name = "Default";
    public float DamageMultiplier;
    public float ReloadSpeedMultiplier;
    public float AttackSpeedMultiplier;
    public float SpeedMultiplier;
    public float HPMultiplier;
}
