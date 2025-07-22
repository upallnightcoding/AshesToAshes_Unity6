using UnityEngine;

public abstract class ProjectileSO : ScriptableObject
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

    public abstract void lunch(Transform player, Transform muzzlePoint);
}
