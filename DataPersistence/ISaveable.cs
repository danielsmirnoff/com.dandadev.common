public interface ISaveable<T>
{
    public T GetSaveData();
    public void LoadSaveData(T data);
}