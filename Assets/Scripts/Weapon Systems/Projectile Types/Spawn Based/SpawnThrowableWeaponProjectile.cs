using Project.WeaponSystems;
using UnityEngine;

public class SpawnThrowableWeaponProjectile : SpawnWeaponProjectile
{


	public override void StartFireProjectile(float damage, float range)
	{
		StartThrowProjectile(damage, range, Vector3.zero);
	}

	public virtual void StartThrowProjectile(float damage, float range, Vector3 force)
	{
		GameObject projectile = Instantiate(ProjectileToSpawn, TargetPositionToSpawn.position, Quaternion.LookRotation(transform.forward));

		projectile.GetComponent<IWeaponProjectileObject>().SetUpPrefab(damage, range);

		projectile.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
	}

}