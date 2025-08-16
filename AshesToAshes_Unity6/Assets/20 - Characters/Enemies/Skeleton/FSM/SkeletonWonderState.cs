using UnityEngine;
using UnityEngine.AI;

public class SkeletonWonderState : FsmState
{
    public static string STATE_NAME = "Wonder";

    
    private bool startWalking = false;

    private SkeletonCntrl skeleton = null;

    public SkeletonWonderState(SkeletonCntrl skeleton) 
        : base(STATE_NAME)
    {
        this.skeleton = skeleton;
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {
        
    }

    public override string OnUpdate(float dt)
    {
        string nextState = STATE_NAME;

        if (skeleton.AgentHasPath())
        {
            if (!startWalking)
            {
                startWalking = true;
                skeleton.StartWalking();
            }

            skeleton.TurnToNextSteeringPoint();

            if (skeleton.DistanceToDestination(0.5f))
            {
                skeleton.MoveToNextWayPoint();
            }

            if (skeleton.DistanceToHero(3.0f))
            {
                nextState = SkeletonChaseState.STATE_NAME;
            }
        }

        return (nextState);
    }
}
