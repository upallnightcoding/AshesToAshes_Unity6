using UnityEngine;
using UnityEngine.AI;

public class SkeletonCntrl : MonoBehaviour
{
    [SerializeField] private Transform follow;
    [SerializeField] private Transform[] wayPoints;

    private NavMeshAgent agent;

    private Fsm fsm = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        fsm = new Fsm();
        fsm.AddState(new SkeletonIdleState(3.0f));
        fsm.AddState(new SkeletonWonderState(agent, wayPoints, transform));
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnUpdate(Time.deltaTime);
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
