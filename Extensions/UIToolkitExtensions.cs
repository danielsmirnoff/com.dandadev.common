using System;
using UnityEngine;
using UnityEngine.UIElements;

public static class UIToolkitExtensions
{
    /// <summary>
    /// Connects a button's click event to a target function.
    /// </summary>
    /// <param name="document">The current document</param>
    /// <param name="buttonName">Name of the button in toolkit</param>
    /// <param name="callback">The function that gets called</param>
    public static void ConnectButton<T>(this VisualElement element, string buttonName, EventCallback<T> callback) 
        where T : EventBase<T>, new()
    {
        if(element == null) return;
        Button button = element.Q<Button>(buttonName);
        button.RegisterCallback(callback);
    }

    /// <summary>
    /// Unregisters the button's click event from a target function.
    /// </summary>
    /// <param name="document">The current document</param>
    /// <param name="buttonName">Name of the button in toolkit</param>
    /// <param name="callback">The function that gets called</param>
    public static void DisconnectButton<T>(this VisualElement element, string buttonName, EventCallback<T> callback) 
        where T : EventBase<T>, new()
    {
        if(element == null) return;
        Button button = element.Q<Button>(buttonName);
        button.UnregisterCallback(callback);
    }
}