using UnityEngine;

public class ProjectileCntrl : MonoBehaviour
{
    private ProjectileSO projectileSO;

    private Transform transform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Set(ProjectileSO projectileSO, Transform parent, Transform muzzlePoint, Transform pos)
    {
        this.projectileSO = projectileSO;

        GameObject go = Instantiate(projectileSO.projectileVFX, muzzlePoint.position, pos.rotation);
        go.transform.SetParent(parent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
