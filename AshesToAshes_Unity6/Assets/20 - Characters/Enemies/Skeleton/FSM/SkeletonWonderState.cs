using UnityEngine;
using UnityEngine.AI;

public class SkeletonWonderState : FsmState
{
    public static string STATE_NAME = "Wonder";

    private NavMeshAgent agent;
  
    private float rotationSpeed = 2.0f;
    private int currentWayPoint;
    private Transform[] wayPoints;
    private int nWayPoints;
    private Transform self;

    public SkeletonWonderState(NavMeshAgent agent, Transform[] wayPoints, Transform self) 
        : base(STATE_NAME)
    {
        this.agent      = agent;
        this.wayPoints  = wayPoints;
        this.self       = self;

        nWayPoints = wayPoints.Length;
        currentWayPoint = 1;
    }

    public override void OnEnter()
    {
        self.position = wayPoints[0].position;
        agent.destination = wayPoints[currentWayPoint].position;
    }

    public override void OnExit()
    {
        
    }

    public override string OnUpdate(float dt)
    {
        if (agent)
        {
            if (agent.hasPath)
            {
                Vector3 direction = agent.steeringTarget - self.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                self.rotation = Quaternion.Lerp(self.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                float distance = Vector3.Distance(self.position, agent.destination);

                if (distance < 0.5f)
                {
                    currentWayPoint = ((currentWayPoint + 1) % nWayPoints);
                    agent.destination = wayPoints[currentWayPoint].position;
                }
            }
        }

        return (STATE_NAME);
    }
}
