using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragableUI : SelectableUI, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public bool CanDrag { get; protected set; } = true;
    public bool CanDrop { get; protected set; } = true;
    
    public Transform DragHolder { get; protected set; }
    
    [Title("Inspector")] 
    [SerializeField] protected bool showDragableEvents = false;

    [Title("Drag and Drop")] 
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
        if(!CanDrag) return;
        visual.blocksRaycasts = false;
        
        visual.transform.SetParent(DragHolder);
        
        OnStartDrag?.Invoke();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if(!CanDrag) return;
        visual.transform.localPosition += (Vector3)(eventData.delta / scaleFactor);
        OnDragTick?.Invoke();
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if(!CanDrag) return;
        visual.blocksRaycasts = true;
        
        visual.transform.SetParent(visualHolder);
        visual.transform.localPosition = Vector3.zero;
        OnStopDrag?.Invoke();
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if(!CanDrop) return;
        OnDropped?.Invoke();
    }
}