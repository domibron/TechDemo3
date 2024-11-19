using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class SingleProjectile : WeaponProjectileBase
	{
		private ProjectileHitLogicBase hitLogic;

		void Start()
		{
			hitLogic = GetComponent<ProjectileHitLogicBase>();

			if (hitLogic == null)
			{
				throw new NullReferenceException("Cannot work with no logic, please add something with " + nameof(ProjectileHitLogicBase) + " to this object!");
			}
		}

		public override void StartFireProjectile(float damage, float range)
		{
			if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
			{
				hitLogic.HitThisObject(hit.collider.gameObject, damage);
			}


			Debug.DrawRay(transform.position, transform.forward * range, Color.red, 10);
		}

		public override void EndFireProjectile()
		{

		}
	}
}
