using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Database<T> : SerializedScriptableObject
{
    [Title("Database Autofill")] 
    public string[] foldersToSearch;
    public bool recursiveSearch;
    [ShowInInspector, ReadOnly] private int databaseCount;
    [SerializeField] private Dictionary<string, T> database = new();

#if UNITY_EDITOR
    [Button("Refresh Database")]
    protected virtual void SearchFolders()
    {

        database.Clear();
        
        //Find all assets in the specified folder
        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", foldersToSearch);
        
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            
            ScriptableObject obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

            if (obj == null) continue;
            
            if (obj is T typedObj)
            {
                string id = GetId(typedObj);
                if (id == null)
                {
                    Debug.LogWarning($"Database does not override GetId!");
                    continue;
                }
                database[id] = typedObj;
            }
        }
        databaseCount = database.Count;

    }
#endif

    protected virtual string GetId(T obj) => null;

    public T GetByID(string id)
    {
        return database.GetValueOrDefault(id);
    }
    
    // public Data GetByID(int id)
    // {
    //     return database[id];
    // }

}