using UnityEngine;
using UnityEngine.AI;

public class SkeletonCntrl : MonoBehaviour
{
    private readonly float ROTATION_SPEED = 5.0f;

    private Transform hero;

    private NavMeshAgent agent;

    private Transform[] wayPoints;

    private Fsm fsm = null;

    private Animator animator = null;

    private int currentWayPoint = 0;
    private int nWayPoints;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.destination = wayPoints[1].position;

        animator = GetComponent<Animator>();
        
        fsm = new Fsm();
        fsm.AddState(new SkeletonIdleState(3.0f));
        fsm.AddState(new SkeletonWonderState(this));
        fsm.AddState(new SkeletonChaseState(this));
    }

    /**
     * Initialize() - 
     */
    public void Initialize(Transform hero, Transform[] wayPoints)
    {
        this.hero       = hero;
        this.wayPoints  = wayPoints;

        nWayPoints = wayPoints.Length;
    }

    void Update()
    {
        fsm.OnUpdate(Time.deltaTime);
    }

    public int FindNearestWayPoint()
    {
        float nearest = 99999.0f;
        int closest = -1;

        for (int i = 0; i < nWayPoints; i++)
        {
            float distance = Vector3.Distance(wayPoints[i].position, transform.position);

            if (distance < nearest)
            {
                nearest = distance;
                closest = i;
            }
        }

        return (closest);
    }

    /**
     * AgentHasPath() -
     */
    public bool AgentHasPath()
    {
        return((agent) && (agent.hasPath));
    }

    /**
     * StartWalking() - 
     */
    public void StartWalking()
    {
        animator.SetBool("walk", true);
    }

    /**
     * DistanceToDestination() - Returns the distance between the next 
     * destination point and the current position of the skeleton.
     */
    public bool DistanceToDestination(float value)
    {
        return (Vector3.Distance(transform.position, agent.destination) < value);
    }

    public bool DistanceToHero(float value)
    {
        return (Vector3.Distance(hero.position, transform.position) < value);
    }

    /**
     * TurnToNextSteeringPoint() - Turns the skeleton to the next steering 
     * point.  The direction is set and the skeleton and then rotated.
     */
    public void TurnToNextSteeringPoint()
    {
        TurnToPoint(agent.steeringTarget);
    }

    public void FollowHero()
    {
        agent.SetDestination(hero.position);

        if (AgentHasPath())
        {
            TurnToPoint(agent.steeringTarget);
        }

    }

    public void TurnToPoint(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * ROTATION_SPEED);
    }

    /**
     * MoveToNextWayPoint() - Move to the next way point in a circular
     * fashion.  
     */
    public void MoveToNextWayPoint()
    {
        currentWayPoint = ((currentWayPoint + 1) % nWayPoints);
        agent.destination = wayPoints[currentWayPoint].position;
    }

    private void OnDrawGizmos()
    {
        if (agent && agent.hasPath)
        {
            for (var i = 0; i < agent.path.corners.Length - 1; i++)
            {
                Color color = Color.black;

                switch(i)
                {
                    case 0: color = Color.red; break;
                    case 1: color = Color.yellow; break;
                    case 2: color = Color.green; break;
                }

                Debug.DrawLine(agent.path.corners[i], agent.path.corners[i + 1], Color.yellow);
            }
        }
    }
}
