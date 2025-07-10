using UnityEngine;

public class DemonNavCntrl : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;

    private int nWayPoints;
    private int currentWayPoint;
    private Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nWayPoints = wayPoints.Length;
        currentWayPoint = 0;
        
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(wayPoints[currentWayPoint].position, transform.position);

        if (dist < 0.5f)
        {
            currentWayPoint = CalcNextWayPoint(currentWayPoint);
        } 
        
        direction = wayPoints[currentWayPoint].position - transform.position;
        direction.y = 0.0f;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1.5f);

        transform.Translate(direction * 2.0f * Time.deltaTime, Space.World);
    }

    private int CalcNextWayPoint(int n)
    {
        return ((n + 1) % nWayPoints);
    }
}
