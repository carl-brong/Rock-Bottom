using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : BaseState
{
    public T PreviousState { get; set; }
    public T CurrentState { get; set; }

    public void Initialize(T startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }

    public void ChangeState(T newState)
    {
        CurrentState.ExitState();
        PreviousState = CurrentState;
        CurrentState = newState;
        CurrentState.EnterState();
    }
}
