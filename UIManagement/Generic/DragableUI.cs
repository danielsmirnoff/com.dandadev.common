using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragableUI : SelectableUI, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public bool CanDrag { get; protected set; }
    public bool CanDrop { get; protected set; }
    
    [Title("Inspector")] 
    [SerializeField] protected bool showDragableEvents = false;

    [Title("Drag and Drop")] 
    [SerializeField] private Transform dragHolder;
    [SerializeField] private Transform visualHolder;
    [SerializeField] protected CanvasGroup visual;
    
    [Title("Selectable Events")]
    [ShowIf("showDragableEvents")]
    public UnityEvent OnStartDrag = new ();
    [ShowIf("showDragableEvents")]
    public UnityEvent OnDragTick = new ();
    [ShowIf("showDragableEvents")]
    public UnityEvent OnStopDrag = new();
    [ShowIf("showDragableEvents")]
    public UnityEvent OnDropped = new();

    protected float scaleFactor;

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        visual.alpha = .5f;
        visual.blocksRaycasts = false;
        OnStartDrag?.Invoke();
        //visual.transform.SetParent(dragHolder);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        visual.transform.localPosition += (Vector3)(eventData.delta / scaleFactor);
        OnDragTick?.Invoke();
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        visual.alpha = 1f;
        visual.blocksRaycasts = true;
        
        visual.transform.localPosition = Vector3.zero;
        OnStopDrag?.Invoke();
        
        //visual.transform.SetParent(visualHolder);
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        OnDropped?.Invoke();
    }
}