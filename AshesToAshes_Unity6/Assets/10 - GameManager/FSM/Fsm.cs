using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm 
{
    private Dictionary<string, FsmState> stateMap = null;
    private FsmState currentState = null;
    private bool changedNewState = true;

    public Fsm()
    {
        stateMap = new Dictionary<string, FsmState>();
    }

    public void OnUpdate(float dt)
    {

        if (changedNewState)
        {
            changedNewState = false;
            currentState.OnEnter();
        }

        string nextState = currentState.OnUpdate(dt);

        if (nextState != currentState.Name)
        {
            currentState.OnExit();
            currentState = stateMap[nextState];
            changedNewState = true;
        }
    }

    public void AddState(FsmState state)
    {
        if (currentState == null)
        {
            currentState = state;
        }

        stateMap.Add(state.Name, state);
    }
}
