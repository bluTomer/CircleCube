using UnityEngine;

public abstract class BaseEnemyConfig : ScriptableObject
{
	public abstract void ResetConfig();
	public abstract BaseEnemy GetNextPrefab();
}
