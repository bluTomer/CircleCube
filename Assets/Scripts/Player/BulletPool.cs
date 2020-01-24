using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private List<Bullet> AvailablePool;
    private List<Bullet> InUsePool;

    private Bullet _prefab;
    private Transform _parent;

    private void Awake()
    {
        AvailablePool = new List<Bullet>();
        InUsePool = new List<Bullet>();
        
        _parent = new GameObject("BulletPool").transform;
        _parent.SetParent(transform);
    }

    public void Setup(Bullet prefab, int prewarm)
    {
        _prefab = prefab;
        
        for (int i = 0; i < prewarm; i++)
        {
            AvailablePool.Add(CreateBullet());
        }
    }

    public Bullet Get()
    {
        if (AvailablePool.Count == 0)
        {
            AvailablePool.Add(CreateBullet());
        }

        var bullet = AvailablePool[0];
        bullet.gameObject.SetActive(true);
        
        AvailablePool.Remove(bullet);
        InUsePool.Add(bullet);

        return bullet;
    }

    public void Return(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        InUsePool.Remove(bullet);
        AvailablePool.Add(bullet);
    }

    private Bullet CreateBullet()
    {
        var bullet = Instantiate(_prefab, _parent);
        bullet.gameObject.SetActive(false);
        bullet.ReturnToPoolEvent += Return;
        bullet.DeClone();
        
        return bullet;
    }
}
