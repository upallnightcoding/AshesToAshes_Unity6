using UnityEngine;

public class DemonNavCntrl : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;

    private int nWayPoints;
    private int currentWayPoint;
    private int nextWayPoint;
    private Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nWayPoints = wayPoints.Length;
        currentWayPoint = 0;
        nextWayPoint = 1;
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(wayPoints[currentWayPoint].position, wayPoints[nextWayPoint].position);

        if (dist < 0.3f)
        {
            currentWayPoint = CalcNextWayPoint(currentWayPoint);
            nextWayPoint = CalcNextWayPoint(nextWayPoint);
        } 
        
        direction = wayPoints[nextWayPoint].position - wayPoints[currentWayPoint].position;
        direction.y = 0.0f;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.21f);
    }

    private int CalcNextWayPoint(int n)
    {
        return ((n + 1) % nWayPoints);
    }
}
