using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LazyUIController : UIController
{
    [Title("Screens")]
    [SerializeField] private List<UIScreenPair> _registeredScreens;

    protected override bool TryCreateMenu(string id)
    {
        foreach (UIScreenPair screen in _registeredScreens)
        {
            if (screen.id == id)
            {
                GameObject obj = Instantiate(screen.prefab);
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class UIScreenPair
{
    public string id;
    public GameObject prefab;
}