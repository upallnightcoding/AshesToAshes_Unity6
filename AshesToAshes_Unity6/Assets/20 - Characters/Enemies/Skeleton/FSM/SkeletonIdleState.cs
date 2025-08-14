using UnityEngine;

public class SkeletonIdleState : FsmState
{
    public static string STATE_NAME = "Idle";

    private float waitPeriod = 3.0f;
    private float seconds = 0.0f;

    public SkeletonIdleState(float waitPeriod) : base(STATE_NAME)
    {
        this.waitPeriod = waitPeriod;
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

        return (seconds > waitPeriod ? SkeletonWonderState.STATE_NAME : STATE_NAME);
    }
}
