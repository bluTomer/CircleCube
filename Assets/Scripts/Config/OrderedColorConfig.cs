using UnityEngine;

[CreateAssetMenu(menuName = "CircleCube/Ordered Color Config")]
public class OrderedColorConfig : BaseColorConfig
{
    public ColorType[] Colors;

    private int currentIndex;
    
    public override void ResetConfig()
    {
        currentIndex = 0;
    }

    public override Color GetNextColor()
    {
        currentIndex = (currentIndex + 1) % Colors.Length;

        return GameManager.Instance.Config.GetColor(Colors[currentIndex]);
    }
}
