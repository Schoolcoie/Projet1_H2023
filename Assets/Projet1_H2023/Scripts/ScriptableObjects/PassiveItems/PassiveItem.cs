using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PassiveItem : ScriptableObject
{
    public Texture2D Icon;
    public string Name = "Default";
    public string Description = "Test";
    public float DamageMultiplier;
    public float AttackSpeedMultiplier;
    public float SpeedMultiplier;
    public float HPMultiplier;
    public float ProjectileSpeedMultiplier;
    public float RangeMultiplier;
    public float AccuracyMultiplier;
    public float ProjectileSizeMultiplier;

    public int ExtraProjectileModifier;

    [System.Serializable]
    public enum EProjectileEffects
    {
        Bomb,
        Pierce,
        Bounce,
        Zigzag,
        RandomSize
    }
    public List<EProjectileEffects> Effects;
}
