using UnityEngine;

namespace CommonDan
{
    public interface ISaveable<T>
    {
        public T GetSaveData();
        public void LoadSaveData(T data);

        public string GetObjectAsJson() {
            return JsonUtility.ToJson(GetSaveData(), true);
        }
    }
}