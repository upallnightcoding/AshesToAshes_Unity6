using UnityEngine;

public class HeroMoveState : FsmState
{
    public static string STATE_NAME = "Idle";

    private HeroCntrl heroCntrl = null;

    public HeroMoveState(HeroCntrl heroCntrl) : base(STATE_NAME)
    {
        this.heroCntrl = heroCntrl;
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {

    }

    public override string OnUpdate(float dt)
    {
        string newState = "";

        if (heroCntrl.IsMoving())
        {
            newState = HeroMoveState.STATE_NAME;
            heroCntrl.Move(dt);
        } else
        {
            newState = HeroIdleState.STATE_NAME;
        }

        return (newState);
    }
}
