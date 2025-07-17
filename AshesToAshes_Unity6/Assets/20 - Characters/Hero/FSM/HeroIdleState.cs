using UnityEngine;

public class HeroIdleState : FsmState
{
    public static string STATE_NAME = "Idle";

    private HeroCntrl heroCntrl = null;

    public HeroIdleState(HeroCntrl heroCntrl) : base(STATE_NAME)
    {
        this.heroCntrl = heroCntrl;
    }

    public override void OnEnter()
    {
        heroCntrl.StopAnimation();
    }

    public override void OnExit()
    {
        
    }

    public override string OnUpdate(float dt)
    {
        return (heroCntrl.IsMoving() ? HeroMoveState.STATE_NAME : HeroIdleState.STATE_NAME);
    }
}
