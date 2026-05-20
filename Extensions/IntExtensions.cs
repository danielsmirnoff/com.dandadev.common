using UnityEngine;

public static class IntExtensions
{
    /// <summary>
    /// Adds an amount to the int, clamps it between min and max.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="amount"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int Add(this int target, int amount, int min, int max)
    {
        return Mathf.Clamp(target + amount, 0, max);
    }
}