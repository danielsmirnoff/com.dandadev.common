using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class Database<T> : SerializedScriptableObject
{
    [Title("Database Autofill")] 
    public string[] foldersToSearch;
    public bool recursiveSearch;
    [SerializeField] private Dictionary<string, T> database = new();

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
    }
    protected virtual string GetId(T obj) => null;

    public T GetByID(string id)
    {
        return database[id];
    }
    
    // public Data GetByID(int id)
    // {
    //     return database[id];
    // }

}