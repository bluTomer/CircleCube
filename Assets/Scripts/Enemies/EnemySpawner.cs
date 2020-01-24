using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public event Action EnemyDestroyedEvent;
    public event Action EnemyBlewUpEvent;
    
    private Vector3 _center;
    private float _radius;
    private float _spawnRate;
    private BaseColorConfig _colorConfig;
    private BaseEnemyConfig _enemyConfig;

    private bool _isSpawning;
    private float _lastSpawnTime;
    private List<BaseEnemy> _enemies = new List<BaseEnemy>();

    public void Setup(Vector3 center, float radius, float spawnRate)
    {
        _center = center;
        _radius = radius;
        _spawnRate = spawnRate;
    }

    public void StartSpawning(BaseEnemyConfig enemyConfig, BaseColorConfig colorConfig)
    {
        _enemyConfig = enemyConfig;
        _colorConfig = colorConfig;
        
        _isSpawning = true;
    }

    public void StopSpawning()
    {
        _isSpawning = false;
    }

    public void DestroyAll()
    {
        foreach (var enemy in _enemies)
        {
            Destroy(enemy.gameObject);
        }
        
        _enemies.Clear();
    }

    private void Update()
    {
        if (!_isSpawning)
            return;

        if (Time.time < _lastSpawnTime + _spawnRate)
            return;

        SpawnEnemy();

        _lastSpawnTime = Time.time;
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(_enemyConfig.GetNextPrefab(), transform);
        enemy.transform.position = GetRandomPosition();

        var config = GameManager.Instance.Config;
        enemy.Setup(config.GrowRate, config.BlowThreshold, config.ErrorMultiplier, _colorConfig.GetNextColor());
        enemy.EnemyDestroyedEvent += OnEnemyDestroyedEvent;
        enemy.EnemyBlewUpEvent += OnEnemyBlewUpEvent;
        
        _enemies.Add(enemy);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 point = Random.insideUnitCircle.normalized * _radius;
        
        point.z = point.y;
        point.y = _center.y;

        return _center + point;
    }

    private void OnEnemyDestroyedEvent(BaseEnemy enemy)
    {
        _enemies.Remove(enemy);
        
        if (EnemyDestroyedEvent != null)
        {
            EnemyDestroyedEvent();
        }
    }

    private void OnEnemyBlewUpEvent(BaseEnemy enemy)
    {
        _enemies.Remove(enemy);
        
        if (EnemyBlewUpEvent != null)
        {
            EnemyBlewUpEvent();
        }
    }
}
