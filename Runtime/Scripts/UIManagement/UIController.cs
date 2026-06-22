using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace CommonDan
{ 
    /// <summary>
    /// A controller class that manages current ui screens and transitions.
    /// </summary>
    public class UIController : MonoBehaviour
    {
        private const bool debugMode = false;
        
        [Title("Menus")]
        [SerializeField] private List<Menu> _registeredMenus;

        private Dictionary<string, Menu> _menus = new();
        private Stack<Menu> _menuStack = new();

        public virtual void Awake()
        {
            foreach (var menu in _registeredMenus)
            {
                RegisterScreen(menu, false);
            }
        }

        public virtual void RegisterScreen(Menu screen, bool registerToList = true)
        {
            if (debugMode) Debug.Log($"Registered {screen.GetMenuID()} as {screen.GetType().Name}");
            if(registerToList) _registeredMenus.Add(screen);
            _menus[screen.GetMenuID()] = screen;
            screen.Initialize(this);
            Hide(screen.GetMenuID());
            if(screen.ShouldShowOnStart()) Show(screen.GetMenuID());
        }

        public virtual void Show(string id)
        {
            if (!_menus.TryGetValue(id, out var screen))
            {
                Debug.LogWarning($"Can't find menu {id} trying to create");
                if(!TryCreateMenu(id)) return;
            }
            screen = GetMenu<Menu>(id);

            // Hide current top if there is one
            //if (_menuStack.TryPeek(out var current)) current.Hide();
            
            foreach (var menu in _registeredMenus)
            {
                if(menu == screen) continue;
                if(menu.ShouldHideOnOtherShow()) menu.Hide();
            }

            //_menuStack.Push(screen);
            screen.Show();
            if (debugMode) Debug.Log($"Showing {screen.GetMenuID()} as {screen.GetType().Name}");
        }

        public virtual void Hide(string id)
        {
            if (!_menus.TryGetValue(id, out var screen))
            {
                Debug.LogWarning($"Can't find menu {id} trying to create");
                if(!TryCreateMenu(id)) return;
            }
            screen = GetMenu<Menu>(id);

            screen.Hide();
            if (debugMode) Debug.Log($"Hiding {screen.GetMenuID()} as {screen.GetType().Name}");
            //_menuStack.TryPop(out _);

            // Restore the screen below if any
            // if (_menuStack.TryPeek(out var previous))
            // {
            //     previous.Show();
            // }
        }

        public virtual void Toggle(string id)
        {
            if (!_menus.TryGetValue(id, out var screen))
            {
                Debug.LogWarning($"Can't find menu {id} trying to create");
                if(!TryCreateMenu(id)) return;
            }
            screen = GetMenu<Menu>(id);
            
            foreach (var menu in _registeredMenus)
            {
                if(menu == screen) continue;
                if(menu.ShouldHideOnOtherShow()) menu.Hide();
            }
            screen.Toggle();
            if (debugMode) Debug.Log($"Toggling {screen.GetMenuID()} as {screen.GetType().Name}");
        }

        public virtual T GetMenu<T>(string id) where T : Menu
        {
            if (!_menus.TryGetValue(id, out var screen))
            {
                Debug.LogWarning($"Can't find menu {id} trying to create");
                if(!TryCreateMenu(id)) return null;
            }
            if(!_menus.TryGetValue(id, out screen))
            {
                Debug.LogWarning($"Can't find menu {id}");
                return null;
            }
            return screen as T;
        }

        /// <returns>Whether or not any menus were identified and closed.</returns>
        /// <param name="doForceClose">Should menus be hidden regardless of what ShouldHideOnOthersShow is set to?</param>
        public virtual bool CloseAllRegisteredMenus(bool doForceClose = false)
        {
            bool foundMenu = false;
            foreach (Menu menu in _registeredMenus)
            {
                if (menu.GetIsVisible())
                    if (menu.ShouldHideOnOtherShow() || doForceClose)
                    {
                        menu.Hide();
                        foundMenu = true;
                    }
            }
            return foundMenu;
        }

        protected virtual bool TryCreateMenu(string id) { return false;}

        public virtual void Back()
        {
            if (_menuStack.TryPop(out var current))
            {
                current.Hide();
            }

            if (_menuStack.TryPeek(out var previous))
            {
                previous.Show();
            }
        }
    }
}
