using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public event Action<Bullet> ReturnToPoolEvent;
    
    public Color Color { get; private set; }
    
    private Rigidbody _rb;
    private bool _enabled;
    private float _moveSpeed;
    private float _setupTime;
    private float _expirationTime;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public void Return()
    {
        _enabled = false;

        if (ReturnToPoolEvent != null)
        {
            ReturnToPoolEvent(this);
        }
    }

    public void Setup(Transform point, Color color, float speed, float expirationTime)
    {
        _moveSpeed = speed;
        _expirationTime = expirationTime;
        _setupTime = Time.time;
        
        SetColor(color);
        
        transform.position = point.position;
        transform.rotation = point.rotation;
        
        _enabled = true;
    }

    private void Update()
    {
        if (_enabled)
        {
            var move = transform.forward * Time.deltaTime * _moveSpeed;
            _rb.MovePosition(transform.position + move);

            // If expired return to pool
            if (Time.time > _setupTime + _expirationTime)
                Return();
        }
    }

    private void SetColor(Color color)
    {
        Color = color;
        GetComponentInChildren<Renderer>().material.SetColor("_Color", color);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Find Enemy object
        var enemy = other.transform.GetComponentInParent<BaseEnemy>();
        if (enemy == null)
            return;

        // Decide if to die or grow according to color
        if (Color == enemy.Color)
        {
            enemy.Die();
        }
        else
        {
            enemy.Grow(isError: true);
        }
        
        // Return this to the pool
        Return();
    }
}
