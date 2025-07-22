using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectile", menuName = "A2A/Red Arden")]
public class LightAttack_RedArden : ProjectileSO
{
    public override void lunch(Transform player, Transform muzzlePoint)
    {
        GameObject projectile = Instantiate(projectilePrefab, muzzlePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(player.forward * force);
        projectile.GetComponent<ProjectileCntrl>().Set(this, muzzlePoint, player);
        Destroy(projectile, duration);
    }
}
