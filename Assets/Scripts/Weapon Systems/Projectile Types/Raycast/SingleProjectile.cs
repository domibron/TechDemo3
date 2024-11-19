using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class SingleProjectile : MonoBehaviour, IWeaponProjectile
	{
		private IProjectileHitLogic hitLogic;

		void Start()
		{
			hitLogic = GetComponent<IProjectileHitLogic>();

			if (hitLogic == null)
			{
				throw new NullReferenceException("Cannot work with no logic, please add something with " + nameof(IProjectileHitLogic) + " to this object!");
			}
		}

		void IWeaponProjectile.StartFireProjectile(float damage, float range)
		{
			if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
			{
				hitLogic.HitThisObject(hit.collider.gameObject, damage);
			}


			Debug.DrawRay(transform.position, transform.forward * range, Color.red, 10);
		}

		void IWeaponProjectile.EndFireProjectile()
		{

		}
	}
}
