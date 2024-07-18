using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour, IStateSwitcher
{
    private readonly List<IState> _states = new();
    private IState _activeState;

    public void Register(IState state)
    {
        if (_states.Contains(state))
            return;

        _states.Add(state);
    }

    public void Switch<T>() where T : IState
    {
        IState state = _states.FirstOrDefault(currentState => currentState is T);

        if (state == null)
            return;

        _activeState?.OnExit();
        _activeState = state;
        _activeState.OnEnter();
    }

    public void Update()
    {
        _activeState?.OnUpdate();
    }
}
