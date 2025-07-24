using UnityEngine;
using UnityEngine.AI;

public class SkeletonCntrl : MonoBehaviour
{
    [SerializeField] private Transform follow;

    private NavMeshAgent agent;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(follow.position);
    }

    /**
     * RandomPosition()
     */
    public Vector3 RandomPosition()
    {
        Vector2 position = Random.insideUnitCircle * 3.0f;

        return (transform.position + new Vector3(position.x, 0.0f, position.y));
    }

    /**
     * isNearTarget() -
     */
    public bool isNearTarget()
    {
        return (false);
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
            animator.SetBool("attack", distance < 3.0f);

            Vector3 direction = (agent.path.corners[1] - transform.position).normalized;
            direction.y = 0.0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10.0f);
        }
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

                Debug.DrawLine(agent.path.corners[i], agent.path.corners[i + 1], color);
            }
        }
    }
}
