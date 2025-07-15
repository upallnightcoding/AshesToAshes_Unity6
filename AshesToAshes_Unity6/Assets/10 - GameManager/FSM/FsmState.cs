using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FsmState 
{
    public string Name { private set; get; }

    public FsmState(string name)
    {
        Name = name;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract string OnUpdate(float dt);
}
