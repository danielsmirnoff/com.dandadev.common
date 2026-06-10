using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A generic persistent singleton pattern.
/// </summary>
/// <typeparam name="T">Type of singleton</typeparam>
[DefaultExecutionOrder(-100)]
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private const bool debugMode = false;
    
    public static T instance;
    
    protected bool _initialized = false;
    public virtual void Awake()
    {
        if (instance != null) {
            _initialized = false;
            if(debugMode) Debug.LogWarning(typeof(T).Name + " is already in the scene!");
            Destroy(gameObject);
            return;
        }
        
        instance = this as T;
        DontDestroyOnLoad(gameObject);
        _initialized = true;
        if(debugMode) Debug.Log(typeof(T).Name + " Created");
    }

    public virtual void Start()
    {
        if(debugMode) Debug.Log(typeof(T).Name + " Started");
        Initialize();
    }

    public virtual void OnEnable()
    {
        SceneManager.sceneLoaded += RebindToLocalScene;
    }

    public virtual void OnDisable()
    {
        SceneManager.sceneLoaded -= RebindToLocalScene;
    }

    /// <summary>
    /// Binds the persistent manager to the local scene on load
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    protected virtual void RebindToLocalScene(Scene scene, LoadSceneMode mode)
    {
        if(debugMode) Debug.Log(typeof(T).Name + " RebindedToLocalScene");
        Initialize();
    }

    /// <summary>
    /// Called once during start and again when loading a new scene
    /// </summary>
    protected virtual void Initialize()
    {
        if(debugMode) Debug.Log(typeof(T).Name + " initialized");
    }
}