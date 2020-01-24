using UnityEngine;

[CreateAssetMenu(menuName = "CircleCube/Ordered Enemy Config")]
public class OrderedEnemyConfig : BaseEnemyConfig
{
	public BaseEnemy[] Prefabs;

	private int currentIndex;
    
	public override void ResetConfig()
	{
		currentIndex = 0;
	}

	public override BaseEnemy GetNextPrefab()
	{
		currentIndex = (currentIndex + 1) % Prefabs.Length;
		return Prefabs[currentIndex];
	}
}
