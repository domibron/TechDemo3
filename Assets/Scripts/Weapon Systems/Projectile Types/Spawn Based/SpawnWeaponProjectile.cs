using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class SpawnWeaponProjectile : MonoBehaviour, IWeaponProjectile
	{
		[Header("Rigidbody Projectile (MUST HAVE A RB)")]
		public GameObject ProjectileToSpawn;

		public float Force = 100f;

		[Tooltip("Can leave blank for this transform")]
		public Transform TargetPositionToSpawn;

		void Start()
		{
			if (TargetPositionToSpawn == null)
			{
				TargetPositionToSpawn = transform;
			}
		}

		void IWeaponProjectile.EndFireProjectile()
		{

		}

		void IWeaponProjectile.StartFireProjectile(float damage, float range)
		{
			GameObject projectile = Instantiate(ProjectileToSpawn, TargetPositionToSpawn.position, Quaternion.LookRotation(transform.forward));

			projectile.GetComponent<IWeaponProjectileObject>().SetUpPrefab(damage, range);

			projectile.GetComponent<Rigidbody>().AddForce(transform.forward * Force, ForceMode.Impulse);
		}
	}
}
