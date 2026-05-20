using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// A controller class that manages current ui screens and transitions.
/// </summary>
public class UIController : MonoBehaviour
{
    public Menu currentMenu {get; private set; }

    [Title("UI Controller")]
    public List<Menu> allMenus;

    private Dictionary<string, Menu> menus = new();

    private void Awake()
    {
        foreach (Menu menu in allMenus)
        {
            menus.Add(menu.GetMenuID(), menu);
            menu.Initialize();
            menu.Hide();
        }
    }

    public void SwitchMenu(Menu menu)
    {
        SetMenu(menu);
    }

    public void SwitchMenu(string menuId)
    {
        SetMenu(menus[menuId]);
    }

    private void SetMenu(Menu menu)
    {
        currentMenu = menu;
        
        if (menu != null)
        {
            currentMenu?.Hide();
            menu.Show();
        }
    }
}