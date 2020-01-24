using UnityEngine;

[CreateAssetMenu(menuName = "CircleCube/Random Color Config")]
public class RandomColorConfig : BaseColorConfig
{
    public override void ResetConfig()
    {
        // Do nothing
    }

    public override Color GetNextColor()
    {
        // Pick a random from 0 to 2 (random on ints is EXCLUSIVE!)
        var value = Random.Range(0, 3);

        var colorType = (ColorType) value;

        return GameManager.Instance.Config.GetColor(colorType);
    }
}
