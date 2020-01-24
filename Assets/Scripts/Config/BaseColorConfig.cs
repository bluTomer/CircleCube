using UnityEngine;

public abstract class BaseColorConfig : ScriptableObject
{
    public abstract void ResetConfig();
    public abstract Color GetNextColor();
}
