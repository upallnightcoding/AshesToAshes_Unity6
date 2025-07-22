using UnityEngine;

public class ProjectileCntrl : MonoBehaviour
{
    private ProjectileSO projectileSO;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Set(ProjectileSO projectileSO, Transform muzzlePoint, Transform hero)
    {
        this.projectileSO = projectileSO;

        GameObject go = Instantiate(projectileSO.projectileVFX, muzzlePoint.position, hero.rotation);
        go.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = Instantiate(projectileSO.impactVFX, transform.position, Quaternion.identity);
        //GameObject go = Instantiate(projectileSO.impactVFX, other.ClosestPoint, Quaternion.identity);
        Destroy(go, 2.0f);

        Destroy(gameObject);
    }
}
