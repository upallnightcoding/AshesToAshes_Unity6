using UnityEngine;

public class SkeletonChaseState : FsmState
{
    public static string STATE_NAME = "Chase";

    public SkeletonChaseState(SkeletonCntrl skeleton)
        : base(STATE_NAME)
    {
        
    }

    public override void OnEnter()
    {
       
    }

    public override void OnExit()
    {
        
    }

    public override string OnUpdate(float dt)
    {
        return ("");
    }
}
