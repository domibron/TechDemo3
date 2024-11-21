using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class SingleReturnableRaycastProjectile : ReturnableWeaponProjectileBase
	{


		[SerializeField]
		private ProjectileHitLogicBase _hitLogic;


		void Start()
		{
			_hitLogic = GetComponent<ProjectileHitLogicBase>();

			if (_hitLogic == null)
			{
				throw new NullReferenceException("Cannot work with no logic, please add something with " + nameof(ProjectileHitLogicBase) + " to this object!");
			}
		}



		public override (Transform, Vector3) StartFireProjectile(float damage, float range)
		{
			if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
			{
				_hitLogic.HitThisObject(hit.collider.gameObject, damage);
				return (hit.transform, hit.point);
			}

			Debug.DrawRay(transform.position, transform.forward * range, Color.red, 10);

			return (null, Vector3.zero);

		}

		public override void EndFireProjectile()
		{

		}
	}
}
