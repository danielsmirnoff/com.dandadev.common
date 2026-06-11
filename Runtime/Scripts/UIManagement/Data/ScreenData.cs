using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// A screen data object class. Fill in with custom logic for events.
/// </summary>
public class ScreenData : ScriptableObject
{
    [Title("Screen Data")]
    public string screenName; //Could be used as a key
    
    [Title("Transition")]
    public float transitionTime;
    
    //Protected
    protected UIController _uiController;
    //protected Menu _menu;
    
    /*
    public virtual void Setup(UIController controller, Menu menu)
    {
        _uiController = controller;
        _menu = menu;
        Hide();
    }
    */

    public virtual void Show()
    {
        //_menu.Show();
    }

    public virtual void Hide()
    {
        //_menu.Hide();
    }
    
}