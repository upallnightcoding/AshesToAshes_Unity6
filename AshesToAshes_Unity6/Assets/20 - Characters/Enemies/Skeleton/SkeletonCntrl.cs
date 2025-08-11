using UnityEngine;
using UnityEngine.AI;

public class SkeletonCntrl : MonoBehaviour
{
    [SerializeField] private Transform follow;
    [SerializeField] private Transform[] wayPoints;

    private NavMeshAgent agent;
    private Animator animator;

    private int nWayPoints;
    private int currentWayPoint;

    private Vector3 ps = Vector3.zero;
    private Vector3 pe = Vector3.zero;

    private float seconds = 0.0f;
    private int currentCorner = 0;

    private bool startingPath = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWayPoint = 1;
        nWayPoints = wayPoints.Length;
        transform.position = wayPoints[0].position;

        agent = GetComponent<NavMeshAgent>();
        agent.destination = wayPoints[currentWayPoint].position;

        animator = GetComponent<Animator>();

       
    }

    // Update is called once per frame
    void Update()
    {
        //Move1(wayPoints[currentWayPoint].position);

        //Wonder();

        if (agent)
        {
            if (agent.hasPath)
            {
                if (startingPath)
                {
                    startingPath = false;
                    currentCorner = 1;
                    pe = agent.path.corners[currentCorner];

                    Vector3 direction = pe - transform.position;
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = targetRotation;
                }

                float distance = Vector3.Distance(transform.position, pe);

                if (distance < 1.0f)
                {
                    Debug.Log($"Way Point: {currentWayPoint}/{currentCorner}/{distance}");
                    currentCorner = currentCorner + 1;

                    if (currentCorner == agent.path.corners.Length)
                    {
                        currentWayPoint = ((currentWayPoint + 1) % nWayPoints);
                        startingPath = true;
                        agent.destination = wayPoints[currentWayPoint].position;
                    } else
                    {
                        pe = agent.path.corners[currentCorner];
                        Vector3 direction = pe - transform.position;
                        Quaternion targetRotation = Quaternion.LookRotation(direction);
                        transform.rotation = targetRotation;
                    }
                }
            }
        }
    }
   
    /**
     * isNearTarget() -
     */
    public bool isNearTarget()
    {
        return (false);
    }

    public void Wonder()
    {
        if (agent)
        {
            if (agent.hasPath)
            {
                float distance = Vector3.Distance(transform.position, agent.destination);

                if (distance < 1.0f)
                {
                    currentWayPoint = (currentWayPoint + 1) % nWayPoints;
                    agent.SetDestination(wayPoints[currentWayPoint].position);

                    animator.SetBool("turnaround", true);

                    //Vector3 direction = wayPoints[point].position - transform.position;
                    //Quaternion targetRotation = Quaternion.LookRotation(direction);
                    //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 50.0f);
                    //transform.rotation = targetRotation;
                }
            } else
            {
                agent.SetDestination(wayPoints[currentWayPoint].position);

                //animator.SetBool("turnaround", true);

                //Vector3 direction = wayPoints[point].position - transform.position;
                //Quaternion targetRotation = Quaternion.LookRotation(direction);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 50.0f);
                //transform.rotation = targetRotation;
            }
        }
    }

    public void Move1(Vector3 position)
    {
        agent.destination = position;

        if (agent && agent.hasPath)
        {
            for (var i = 0; i < agent.path.corners.Length - 1; i++)
            {
                Vector3 direction = (agent.path.corners[i + 1] - agent.path.corners[i]).normalized;
                direction.y = 0.0f;

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10.0f);

                float distance = Vector3.Distance(transform.position, agent.destination);
                if (distance < 1.0f)
                {
                    currentWayPoint = CalcNextWayPoint(nWayPoints);
                }
            }
         }
    }

    /**
     * Move()
     */
    public void Move(Vector3 position)
    {
        agent.destination = position;

        if (agent && agent.hasPath)
        {
            float distance = Vector3.Distance(transform.position, agent.destination);
            //animator.SetBool("attack", distance < 1.0f);

            Vector3 direction = (agent.path.corners[1] - transform.position).normalized;
            direction.y = 0.0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10.0f);

            if (distance < 1.0f)
            {
                currentWayPoint = CalcNextWayPoint(nWayPoints);
            }
        }
    }

    private int CalcNextWayPoint(int n)
    {
        return ((n + 1) % nWayPoints);
    }

    /**
    * RandomPosition()
    */
    public Vector3 RandomPosition()
    {
        Vector2 position = Random.insideUnitCircle * 3.0f;

        return (transform.position + new Vector3(position.x, 0.0f, position.y));
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
