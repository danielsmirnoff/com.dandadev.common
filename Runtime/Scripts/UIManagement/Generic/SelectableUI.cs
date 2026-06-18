using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CommonDan
{
    public class SelectableUI  : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Title("Inspector")] 
        [SerializeField] private bool showSelectionEvents = false;
    
        [Title("Selectable Events")]
        [ShowIf("showSelectionEvents")]
        public UnityEvent OnStartHover = new();
        [ShowIf("showSelectionEvents")]
        public UnityEvent OnEndHover = new();
        [ShowIf("showSelectionEvents")]
        public UnityEvent OnClick = new();
    
        public virtual void Start()
        {
        }
    
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            OnStartHover?.Invoke();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            OnEndHover?.Invoke();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }
    }
}