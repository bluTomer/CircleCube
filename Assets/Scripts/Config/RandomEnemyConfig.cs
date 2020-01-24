using UnityEngine;

[CreateAssetMenu(menuName = "CircleCube/Random Enemy Config")]
public class RandomEnemyConfig : BaseEnemyConfig
{
	public BaseEnemy[] Prefabs;
	
	public override void ResetConfig()
	{
		// Do nothing
	}

	public override BaseEnemy GetNextPrefab()
	{
		var value = Random.Range(0, Prefabs.Length);
		return Prefabs[value];
	}
}
