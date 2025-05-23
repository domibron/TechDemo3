using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class SpawnWeaponProjectile : WeaponProjectileBase
	{
		[Header("Rigidbody Projectile (MUST HAVE A RB)")]
		public GameObject ProjectileToSpawn;


		[Tooltip("Can leave blank for this transform")]
		public Transform TargetPositionToSpawn;

		private Transform _camTransform;

		protected virtual void Start()
		{
			if (TargetPositionToSpawn == null)
			{
				TargetPositionToSpawn = transform;
			}

			_camTransform = Camera.main.transform;
		}

		public override void EndFireProjectile()
		{

		}

		public override void StartFireProjectile(float damage, float range)
		{
			GameObject projectile = Instantiate(ProjectileToSpawn, TargetPositionToSpawn.position, Quaternion.LookRotation(_camTransform.transform.forward));

			projectile.GetComponent<IWeaponProjectileObject>().SetUpPrefab(damage, range);

		}
	}
}
