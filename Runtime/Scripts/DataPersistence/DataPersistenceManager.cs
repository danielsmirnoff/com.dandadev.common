using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager instance { get; private set; }
    
    private static string fileName = "save";
    
    [Header("File Storage Config")]
    [HideInInspector] public string saveName;
    [HideInInspector] public int saveID;

    public UnityEvent<GameData> OnSave { get; private set; } = new();
    public UnityEvent<GameData> OnLoad { get; private set; } = new();

    private List<IDataPersistence> dataPersistenceObjects;
    private List<IDataPersistence> additionalDataPersistenceObjects = new();
    private GameData gameData;
    private FileDataHandler dataHandler;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one Data instance managers found!");
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        FindAllDataPersistenceObjects();
    }

    protected virtual GameData CreateNewData()
    {
        return new GameData();
    }

    #region Save Management

    public void SetSaveID(string saveName, int saveID)
    {
        this.saveName = saveName;
        this.saveID = saveID;
    }
    
    public void DeleteSave(string saveName, int saveID)
    {
        dataHandler.Delete(saveID);
    }
    
    public void NewGame()
    {
        gameData = CreateNewData();
    }

    
    public GameData GetSaveData(int saveID)
    {
        return dataHandler.Load(saveID);
    }

    public GameData GetCurrentSaveData()
    {
        return GetSaveData(saveID);
    }
    
    public void LoadCurrentGame()
    {
        LoadGame(saveID);
    }
    
    public void SaveCurrentGame()
    {
        SaveGame(saveID);
    }
    #endregion




    private void LoadGame(int saveID)
    {
        gameData = dataHandler.Load(saveID);

        if (gameData == null)
        {
            Debug.Log("No Save Data found.");
            NewGame();
        }

        foreach (IDataPersistence obj in dataPersistenceObjects)
        {
            obj.LoadData(gameData);
        }
        foreach (IDataPersistence obj in additionalDataPersistenceObjects)
        {
            obj.LoadData(gameData);
        }
        
        OnLoad.Invoke(gameData);
        Debug.Log("Game Loaded.");
    }

    private void SaveGame(int saveID)
    {
        if (gameData == null)
        {
            Debug.Log("No Save Data found. Creating new save.");
            NewGame();
        }
        
        //Save all registered objects
        foreach (IDataPersistence obj in dataPersistenceObjects)
        {
            obj.SaveData(gameData);
        }
        foreach (IDataPersistence obj in additionalDataPersistenceObjects)
        {
            obj.SaveData(gameData);
        }
        OnSave.Invoke(gameData);
        
        //Write save
        dataHandler.Save(gameData, saveID);
        Debug.Log("Game Saved.");
    }
    

    public void RegisterDataPersistenceObject(IDataPersistence obj)
    {
        additionalDataPersistenceObjects.Add(obj);
    }

    public void UnRegisterDataPersistenceObject(IDataPersistence obj)
    {
        additionalDataPersistenceObjects.Remove(obj);
    }
    
    private void FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<IDataPersistence>();

        this.dataPersistenceObjects = new List<IDataPersistence>(dataPersistenceObjects);
    }

    private void OnApplicationQuit()
    {
        SaveGame(saveID);
    }

}
