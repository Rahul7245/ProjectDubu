using System.Collections.Generic;
using System;
public class StateMachine<TEnum> where TEnum : struct, Enum
{
    private IState _currentState;
    private Dictionary<TEnum, IState> _states = new Dictionary<TEnum, IState>();

    public void AddState(TEnum stateKey, IState state)
    {
        _states[stateKey] = state;
    }

    public void ChangeState(TEnum stateKey)
    {
        if (!_states.ContainsKey(stateKey))
        {
            throw new InvalidOperationException($"State {stateKey} has not been added to the state machine.");
        }

        if (_currentState != null)
            _currentState.OnExit();

        _currentState = _states[stateKey];
        _currentState.OnEnter();
    }

    public void Update()
    {
        _currentState?.OnUpdate();
    }

    public TEnum? GetCurrentStateKey()
    {
        if (_currentState == null) return null;
        
        foreach (var kvp in _states)
        {
            if (kvp.Value == _currentState)
                return kvp.Key;
        }
        
        return null;
    }
}

