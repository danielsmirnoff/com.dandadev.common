using UnityEngine;

/// <summary>
/// Local Singletons only exist in the current scene without the needs to persist
/// </summary>
/// <typeparam name="T"></typeparam>
[DefaultExecutionOrder(-50)]
public class LocalSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;
    
    protected bool _initialized = false;
    public virtual void Awake()
    {
        if (instance != null && instance != this) {
            _initialized = false;
            Debug.LogWarning(typeof(T).Name + " is already in the scene!");
            Destroy(gameObject);
            return;
        }
        
        instance = this as T;
        _initialized = true;
    }
}