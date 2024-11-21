using Project.WeaponSystems;
using UnityEngine;

public class SpawnPysicsWeaponProjectile : SpawnWeaponProjectile
{


	public override void StartFireProjectile(float damage, float range)
	{
		StartPysProjectile(damage, range, Vector3.zero);
	}

	public virtual void StartPysProjectile(float damage, float range, Vector3 force)
	{
		GameObject projectile = Instantiate(ProjectileToSpawn, TargetPositionToSpawn.position, Quaternion.LookRotation(transform.forward));

		projectile.GetComponent<IWeaponProjectileObject>().SetUpPrefab(damage, range);

		projectile.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
	}

}