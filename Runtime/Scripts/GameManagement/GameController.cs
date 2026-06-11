using System;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// A game controller class that manages game states and logic.
/// </summary>
public abstract class GameController
{
    //Private
    private GameStateData _currentState;
    private GameStateData _previousState;
    
    public GameStateData GetCurrentState() => _currentState;
    
    public virtual void SwitchState(GameStateData newState)
    {
        _currentState?.ExitState();
        _previousState = _currentState;
        _currentState = newState;
        _currentState?.Setup();
        _currentState?.EnterState();
    }

    public virtual void UpdateState()
    {
        _currentState?.UpdateState();
    }
}