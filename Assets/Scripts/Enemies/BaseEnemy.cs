using System;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public event Action<BaseEnemy> EnemyDestroyedEvent;
    public event Action<BaseEnemy> EnemyBlewUpEvent;
    
    public Color Color { get; private set; }
    
    protected float _growSpeed;
    protected float _blowThreshold;
    protected float _errorMultiplier;

    private bool _isDying;

    public void Setup(float growSpeed, float blowThreshold, float errorMultiplier, Color color)
    {
        _growSpeed = growSpeed;
        _blowThreshold = blowThreshold;
        _errorMultiplier = errorMultiplier;
        SetColor(color);
    }

    // Returns the correct material to color, an inheriting class must implement this function
    protected abstract Material GetMaterial();

    // Returns the size of the growing transform, an inheriting class must implement this function
    protected abstract float GetGrowSize();

    // Grows the enemy, an inheriting class must implement this function
    public abstract void Grow(bool isError = false);

    protected virtual void SetColor(Color color)
    {
        // Save the color for the collision test later
        Color = color;
        
        // Get the correct material and apply the color to it
        GetMaterial().SetColor("_Color", color);
    }

    public virtual void Die()
    {
        _isDying = true;
        if (EnemyDestroyedEvent != null)
        {
            EnemyDestroyedEvent(this);
        }
    }

    protected virtual void BlowUp()
    {
        _isDying = true;
        if (EnemyBlewUpEvent != null)
        {
            EnemyBlewUpEvent(this);
        }
    }

    protected virtual void Update()
    {
        // Don't grow if currently dying
        if (_isDying)
            return;
        
        // Grow the enemy
        Grow();
        
        // Test if too big
        if (GetGrowSize() > _blowThreshold)
        {
            BlowUp();
        }
    }
}
