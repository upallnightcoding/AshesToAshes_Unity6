using UnityEngine;
using UnityEngine.AI;

public class DemonNavCntrl : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;

    private int nWayPoints;
    private int currentWayPoint;
    private Vector3 direction;
    private NavMeshAgent agent;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        nWayPoints = wayPoints.Length;
        currentWayPoint = 0;
        
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = wayPoints[currentWayPoint].position;
        //animator.SetBool("isRunning", true);

        if (agent.velocity.magnitude != 0)
        {
            animator.SetBool("isRunning", true);
            //agent.isStopped = true;
        } else
        {
            animator.SetBool("isRunning", false);
            //agent.isStopped = false;
        }

        float dist = Vector3.Distance(wayPoints[currentWayPoint].position, transform.position);

        if (dist < 0.5f)
        {
            currentWayPoint = CalcNextWayPoint(currentWayPoint);
        } 
        
    }

    private void OnAnimatorMove()
    {
        if (animator.GetBool("isRunning") == false)
        {
            //agent.speed = (animator.deltaPosition / Time.deltaTime).magnitude;
        }
    }

    private int CalcNextWayPoint(int n)
    {
        return ((n + 1) % nWayPoints);
    }
}
