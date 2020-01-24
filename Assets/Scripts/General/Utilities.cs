using UnityEngine;

public static class Utilities
{
    public static void DeClone(this Component component)
    {
        component.name = component.name.Replace("(Clone)", string.Empty);
    }
}
