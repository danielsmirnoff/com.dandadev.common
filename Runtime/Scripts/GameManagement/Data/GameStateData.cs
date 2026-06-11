using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// A game state data object. Fill with custom logic for entering and exiting the state.
/// </summary>
public class GameStateData : ScriptableObject
{
    [Title("Game State Data")]
    public string stateName = "StateName";

    public virtual void Setup()
    {
        
    }
    
    public virtual void EnterState()
    {
        Debug.Log("Entering " + stateName);
    }

    public virtual void UpdateState()
    {
        
    }
    
    public virtual void ExitState()
    {
    }
}