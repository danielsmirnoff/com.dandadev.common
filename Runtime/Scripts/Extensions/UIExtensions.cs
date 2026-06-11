using UnityEngine;

public static class UIExtensions
{
    public static void ShowGroup(this CanvasGroup group)
    {
        if(group == null) return;
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    public static void HideGroup(this CanvasGroup group)
    {
        if(group == null) return;
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
}