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
        animator = GetComponent<Animator>();

        nWayPoints = wayPoints.Length;
        currentWayPoint = 0;

        agent = GetComponent<NavMeshAgent>();
        agent.destination = wayPoints[currentWayPoint].position;
        agent.isStopped = false;
        agent.speed = 1.5f;
        animator.SetBool("isRunning", true);

        direction = Vector3.zero;
    }

    void Update()
    {
        float dist = Vector3.Distance(agent.transform.position, wayPoints[currentWayPoint].position);
        Debug.Log($"dist: {dist}/{agent.transform.position}/{wayPoints[currentWayPoint].position}");

        if (dist < 0.5f)
        {
            currentWayPoint = CalcNextWayPoint(currentWayPoint);
            agent.destination = wayPoints[currentWayPoint].position;
        }
    }

    // Update is called once per frame
    void xxUpdate()
    {
        animator.SetBool("isRunning", true);

        if (agent.velocity.magnitude != 0)
        {
            //animator.SetBool("isRunning", true);
            //agent.isStopped = true;
        } else
        {
            //animator.SetBool("isRunning", false);
        }

        float dist = Vector3.Distance(agent.transform.position, wayPoints[currentWayPoint].position);
        Debug.Log($"dist: {dist}/{agent.transform.position}/{wayPoints[currentWayPoint].position}");

        if (dist < 0.5f)
        {
            currentWayPoint = CalcNextWayPoint(currentWayPoint);
        } else
        {
            agent.destination = wayPoints[currentWayPoint].position;
        }

        Debug.Log($"Destination: {agent.destination}/{agent.velocity.magnitude}");
        
    }

    private void xxOnAnimatorMove()
    {
        Debug.Log($"OnAnimationMove: {animator.GetBool("isRunning")}");

        /*if (animator.GetBool("isRunning") == true)
        {
            Debug.Log($"deltaPosition: {animator.deltaPosition}/{Time.deltaTime}");
            agent.speed = (animator.deltaPosition / Time.deltaTime).magnitude;
        }*/
    }

    private void xxxOnAnimatorMove()
    {
        Debug.Log($"OnAnimationMove: {animator.GetBool("isRunning")}");

        if (animator.GetBool("isRunning") == true)
        {
            Debug.Log($"deltaPosition: {animator.deltaPosition}/{Time.deltaTime}");
            agent.speed = (animator.deltaPosition / Time.deltaTime).magnitude;
        }
    }

    private int CalcNextWayPoint(int n)
    {
        return ((n + 1) % nWayPoints);
    }
}
