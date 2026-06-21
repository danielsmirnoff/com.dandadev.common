using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace CommonDan
{
    /// <summary>
    /// A generic menu class to handle the view logic
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class Menu : MonoBehaviour
    {
        [Tooltip("Only needed if not a unique class")]
        [SerializeField] private string id;
        [SerializeField] private bool showOnStart;
        [SerializeField] private bool hideOnOtherShow = true;

        // Events
        public UnityEvent OnMenuOpened { get; private set; } = new();
        public UnityEvent OnMenuClosed { get; private set; } = new();
    
        public virtual string GetMenuID() => id;
        public virtual bool ShouldHideOnOtherShow() => hideOnOtherShow;
        public virtual bool ShouldShowOnStart() => showOnStart;
        public bool GetIsVisible() => isVisible;

        //Protected
        protected bool isVisible = false;
        /// <returns>Whether or not to show / hide the attached CanvasGroup when the menu is toggled.</returns>
        protected virtual bool AutoCanvasGroup() => true;

        //Protected
        protected CanvasGroup _canvasGroup;
        protected UIController _uiController;

        public virtual void Initialize(UIController uiController)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _uiController = uiController;
        }

        public virtual void Show()
        {
            isVisible = true;
            if (AutoCanvasGroup())
                _canvasGroup.ShowGroup();
            OnMenuOpened.Invoke();
        }

        public virtual void Hide()
        {
            isVisible = false;
            if (AutoCanvasGroup())
                _canvasGroup.HideGroup();
            OnMenuClosed.Invoke();
        }
    
        public virtual void Toggle()
        {
            isVisible = !isVisible;
            if (isVisible)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        
    }
}