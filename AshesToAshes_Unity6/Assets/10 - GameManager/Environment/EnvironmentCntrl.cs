using UnityEngine;
using Unity.AI.Navigation;

public class EnvironmentCntrl : MonoBehaviour
{
    private NavMeshSurface nms = null;

    private void Awake()
    {
        nms = GetComponent<NavMeshSurface>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nms.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
