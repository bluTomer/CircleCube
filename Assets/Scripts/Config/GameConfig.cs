using System;
using UnityEngine;

[CreateAssetMenu(menuName = "CircleCube/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("General")]
    public LayerMask InputLayers;
    public Environment EnvironmentPrefab;
    public UI UIPrefab;
    public Color RedColor;
    public Color GreenColor;
    public Color BlueColor;
    public int EnemiesToWin;

    [Header("Player")]
    public Player PlayerPrefab;
    public ColorType StartingColor;
    public float ShotCooldown;
    
    [Header("Bullets")]
    public Bullet BulletPrefab;
    public float BulletSpeed;
    public float BulletExpirationTime;
    public int PoolPrewarmAmount;

    [Header("Spawning")] 
    public BaseEnemyConfig EnemyConfig;
    public BaseColorConfig ColorConfig;
    public float SpawnCircleRadius;
    public float SpawnRate;
    
    [Header("Enemies")]
    public float GrowRate;
    public float BlowThreshold;
    public float ErrorMultiplier;

    public Color GetColor(ColorType type)
    {
        switch (type)
        {
            case ColorType.Red:
                return RedColor;
            case ColorType.Green:
                return GreenColor;
            case ColorType.Blue:
                return BlueColor;
            default:
                throw new ArgumentOutOfRangeException("type", type, null);
        }
    }
}

public enum ColorType
{
    Red   = 0,
    Green = 1,
    Blue  = 2,
}