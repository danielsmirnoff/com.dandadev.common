using Sirenix.OdinInspector;
using UnityEngine;

public class Data : ScriptableObject
{
    [Title("Data")] 
    public int numID;
    public string id;

#if UNITY_EDITOR
    public virtual void OnValidate()
    {
        if (string.IsNullOrEmpty(id))
        {
            //id = System.Guid.NewGuid().ToString();
            id = CreateItemID();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
#endif
    
    protected virtual string CreateItemID()
    {
        return "";
    }
}