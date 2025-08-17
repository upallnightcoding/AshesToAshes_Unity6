using UnityEngine;
using Unity.AI.Navigation;
using System.Collections;

public class EnvironmentCntrl : MonoBehaviour
{
    [SerializeField] private GameObject heroPrefab;
    [SerializeField] private GameObject skeletonPrefab;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private GameObject mainCamera;

    private NavMeshSurface navMeshSurface = null;

    private void Awake()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshSurface.BuildNavMesh();

        CreateObjects();

        //heroPrefab.SetActive(true);
        //skeletonPrefab.SetActive(true);
    }

    private void CreateObjects()
    {
        GameObject hero = Instantiate(heroPrefab, new Vector3(2.0f, 0.0f, 0.0f), Quaternion.identity);
        hero.GetComponent<HeroCntrl>().Initialize(mainCamera);

        //yield return null;

        GameObject skeleton = Instantiate(skeletonPrefab, wayPoints[0].position, Quaternion.identity);
        skeleton.GetComponent<SkeletonCntrl>().Initialize(hero.transform, wayPoints);

        skeleton = Instantiate(skeletonPrefab, wayPoints[0].position + new Vector3(2.0f, 0.0f, 0.0f), Quaternion.identity);
        skeleton.GetComponent<SkeletonCntrl>().Initialize(hero.transform, wayPoints);

        skeleton = Instantiate(skeletonPrefab, wayPoints[0].position + new Vector3(-2.0f, 0.0f, 0.0f), Quaternion.identity);
        skeleton.GetComponent<SkeletonCntrl>().Initialize(hero.transform, wayPoints);

        //yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
