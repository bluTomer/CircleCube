using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public event Action EnemyDestroyedEvent;
    
    // Singleton
    public static GameManager Instance { get; private set; }
    
    // Properties
    public GameConfig Config { get; private set; }
    public int DestroyedEnemies { get; private set; }
    
    // Inspector
    [SerializeField] private string _configName;
    
    // Privates
    private Transform _gameCenter;
    private EnemySpawner _spawner;
    private InputHandler _input;
    private Camera _camera;
    private Player _player;
    private UI _ui;
    
    #region Init

    private void Awake()
    {
        SetupSingleton();
        
        LoadConfig(_configName);

        SetupWorld();
        SetupPlayer();
        SetupSpawner();
        SetupUI();
    }

    private void SetupSingleton()
    {
        if (Instance != null)
            throw new Exception("More than one singleton exists in the scene!");

        Instance = this;
    }

    private void LoadConfig(string configName)
    {
        Config = Resources.Load<GameConfig>("GameConfig/" + configName);
    }
    
    private void SetupWorld()
    {
        var env = Instantiate(Config.EnvironmentPrefab);
        env.DeClone();
        
        _gameCenter = env.GameCenter;
        _camera = env.Camera;
        
        _input = gameObject.AddComponent<InputHandler>();
        _input.Setup(_camera, Config.InputLayers);
        _input.enabled = false;
    }

    private void SetupPlayer()
    {
        var bulletPool = gameObject.AddComponent<BulletPool>();
        bulletPool.Setup(Config.BulletPrefab, Config.PoolPrewarmAmount);
        
        _player = Instantiate(Config.PlayerPrefab, _gameCenter);
        _player.transform.SetParent(null, worldPositionStays: true);
        _player.DeClone();
        
        _player.Setup(bulletPool, Config.BulletSpeed, Config.ShotCooldown, Config.GetColor(Config.StartingColor),
            Config.BulletExpirationTime);
    }

    private void SetupSpawner()
    {
        _spawner = new GameObject("_Enemies").AddComponent<EnemySpawner>();
        _spawner.Setup(_gameCenter.position, Config.SpawnCircleRadius, Config.SpawnRate);
    }

    private void SetupUI()
    {
        _ui = Instantiate(Config.UIPrefab);
        _ui.DeClone();
        _ui.SetMenuMode(Menu.MenuType.Start);
    }
    
    #endregion
    
    #region Game Control

    public void SetColor(ColorType colorType)
    {
        _player.SetColor(Config.GetColor(colorType));
    }

    public void StartGame()
    {
        // Listen to input
        _input.MousePositionEvent += OnMousePositionEvent;
        _input.MouseClickEvent += OnMouseClickEvent;
        _input.enabled = true;
        
        // Start Spawner
        _spawner.StartSpawning(Config.EnemyConfig, Config.ColorConfig);
        _spawner.EnemyBlewUpEvent += OnEnemyBlewUpEvent;
        _spawner.EnemyDestroyedEvent += OnEnemyDestroyedEvent;
        
        // Other
        DestroyedEnemies = 0;
        _ui.SetHUDMode();
    }

    private void EndGame()
    {
        // Stop listening to input
        _input.MousePositionEvent -= OnMousePositionEvent;
        _input.MouseClickEvent -= OnMouseClickEvent;
        _input.enabled = false;
        
        // Stop Spawner
        _spawner.EnemyBlewUpEvent -= OnEnemyBlewUpEvent;
        _spawner.EnemyDestroyedEvent -= OnEnemyDestroyedEvent;
        _spawner.StopSpawning();
        _spawner.DestroyAll();
    }

    private void OnEnemyBlewUpEvent()
    {
        // Game lost!
        EndGame();
        _ui.SetMenuMode(Menu.MenuType.Lose);
    }

    private void OnEnemyDestroyedEvent()
    {
        DestroyedEnemies++;

        if (EnemyDestroyedEvent != null)
        {
            EnemyDestroyedEvent();
        }
        
        if (DestroyedEnemies >= Config.EnemiesToWin)
        {
            // Game Won!
            EndGame();
            _ui.SetMenuMode(Menu.MenuType.Win);
        }
    }
    
    #endregion
    
    #region Event Handlers
    
    private void OnMousePositionEvent(Vector3 point)
    {
        var direction = (point - _player.transform.position).normalized;
        _player.Rotate(point);
    }

    private void OnMouseClickEvent()
    {
        _player.Shoot();
    }
    
    #endregion
}
