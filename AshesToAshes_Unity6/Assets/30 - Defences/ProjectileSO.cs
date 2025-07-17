using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectile", menuName = "A2A/projectile")]
public class ProjectileSO : ScriptableObject
{
    public string projectileName;

    public GameObject projectilePrefab;

    // Amount of damage the projectile can do
    public float damage;

    // Expected life of the projectile
    public float duration;

    public GameObject projectileVFX;
    public GameObject impactVFX;

    // The amount of force that is behind the projectile
    public float force;
}
