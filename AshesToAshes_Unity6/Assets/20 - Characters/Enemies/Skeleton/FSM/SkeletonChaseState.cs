using UnityEngine;

public class SkeletonChaseState : FsmState
{
    public static string STATE_NAME = "Chase";

    private SkeletonCntrl skeleton = null;

    public SkeletonChaseState(SkeletonCntrl skeleton)
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
        skeleton.FollowHero();

        return (skeleton.DistanceToHero(3.0f) ? STATE_NAME : SkeletonWonderState.STATE_NAME);
    }
}
