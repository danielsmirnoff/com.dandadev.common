
/// <summary>
/// A generic object class that holds an instance of an item
/// Instances can store unique values and be serialized for save/load functionality
/// Take in an object of type T to use as a template. Template data is used for static definitions
/// </summary>
/// <typeparam name="T">T is of type template</typeparam>
[System.Serializable]
public class ObjectInstance : IDataPersistence
{
    public readonly Data data;
    public int numID;
    public string objectID;
    
    public ObjectInstance(Data data)
    {
        this.data = data;
        numID = data.numID;
        objectID = data.id;
    }
    
    public virtual void LoadData(GameData data)
    {
        throw new System.NotImplementedException();
    }

    public virtual void SaveData(GameData data)
    {
        throw new System.NotImplementedException();
    }
}

// [Serializable]
// public abstract class ObjectInstance
// {
//     
// }