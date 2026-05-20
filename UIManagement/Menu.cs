using System;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// A generic menu class to handle the view logic
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
    public virtual string GetMenuID() => "Empty";

    //Protected
    protected bool isVisible = false;

    //Private
    private CanvasGroup _canvasGroup;

    public virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Initialize() {}

    public virtual void Show()
    {
        isVisible = true;
        _canvasGroup.ShowGroup();
    }

    public virtual void Hide()
    {
        isVisible = false;
        _canvasGroup.HideGroup();
    }
    
    public virtual void Toggle()
    {
        isVisible = !isVisible;
        if (isVisible)
        {
            _canvasGroup.ShowGroup();
        }
        else
        {
            _canvasGroup.HideGroup();
        }
    }
}