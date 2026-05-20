using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Title("Menus")]
    public List<Menu> menus;
    
    private readonly Dictionary<string, Menu> menuLookup = new();

    private void Start()
    {
        foreach (Menu menu in menus)
        {
            if (menuLookup.ContainsKey(menu.name))
            {
                Debug.LogError($"Menu with name {menu.name} already exists!");
                continue;
            }
            menuLookup.Add(menu.name, menu);
        }
    }
}