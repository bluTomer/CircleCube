using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform FiringPoint;
    
    private BulletPool _bulletPool;
    private float _shotSpeed;
    private float _shotCooldown;

    private Color _currentColor;
    private float _lastShotTime;
    private float _shotExpirationTime;

    private Material[] _materials;

    private void Awake()
    {
        var renderers = GetComponentsInChildren<Renderer>();
        _materials = new Material[renderers.Length];
        for (var i = 0; i < renderers.Length; i++)
        {
            _materials[i] = renderers[i].material;
        }
    }
    
    public void Setup(BulletPool pool, float shotSpeed, float shotCooldown, Color startingColor, float shotExpirationTime)
    {
        _bulletPool = pool;
        _shotSpeed = shotSpeed;
        _shotCooldown = shotCooldown;
        _shotExpirationTime = shotExpirationTime;
        SetColor(startingColor);
    }

    public void SetColor(Color color)
    {
        _currentColor = color;
        foreach (var material in _materials)
        {
            material.SetColor("_Color", color);
        }
    }
    
    public void Shoot()
    {
        if (Time.time < _lastShotTime + _shotCooldown)
            return;
        
        var bullet = _bulletPool.Get();
        bullet.Setup(FiringPoint, _currentColor, _shotSpeed, _shotExpirationTime);

        _lastShotTime = Time.time;
    }
    
    public void Rotate(Vector3 direction)
    {
        direction.y = 0.0f;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}
