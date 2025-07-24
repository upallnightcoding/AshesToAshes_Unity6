using UnityEngine;

public class SkeletonIdleState : FsmState
{
    public static string STATE_NAME = "Idle";

    private float waitPeriod = 3.0f;
    private float seconds = 0.0f;

    public SkeletonIdleState() : base(STATE_NAME)
    {

    }

    public override void OnEnter()
    {
        seconds = 0.0f;
    }

    public override void OnExit()
    {
        
    }

    public override string OnUpdate(float dt)
    {
        seconds += dt;

        return (seconds > waitPeriod ? SkeletonWonderState.STATE_NAME : SkeletonIdleState.STATE_NAME);
    }
}
