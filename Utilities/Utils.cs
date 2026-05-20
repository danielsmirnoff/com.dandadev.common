using UnityEngine;
using UnityEngine.InputSystem;

public static class Utils
{
    /// <summary>
    /// Gets the mouse position using the new Unity input system
    /// </summary>
    /// <param name="customDepth">Custom Depth is used to avoid issues when using screentoworld</param>
    /// <returns></returns>
    public static Vector3 GetMousePos(float customDepth = 10)
    {
        Vector3 pos = Mouse.current.position.ReadValue();
        pos.z = customDepth;
        return pos;
    }
}