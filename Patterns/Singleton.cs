using UnityEngine;

/// <summary>
/// A generic singleton pattern.
/// </summary>
/// <typeparam name="T">Type of singleton</typeparam>
[DefaultExecutionOrder(-100)]
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
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
        DontDestroyOnLoad(gameObject);
        _initialized = true;
    }
}