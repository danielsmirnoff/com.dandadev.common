using UnityEngine;

namespace CommonDan
{
    public interface IDataPersistence
    {
        void LoadData(GameData data)
        {
        }

        void SaveData(GameData data);
    }
}