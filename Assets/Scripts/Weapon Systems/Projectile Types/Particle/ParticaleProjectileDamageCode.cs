using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class ParticaleProjectileDamageCode : MonoBehaviour
	{
		private IProjectileHitLogic hitLogic;

		private float _damage;


		void Start()
		{
			hitLogic = GetComponent<IProjectileHitLogic>();

			if (hitLogic == null)
			{
				throw new NullReferenceException("Cannot work with no logic, please add something with " + nameof(IProjectileHitLogic) + " to this object!");
			}
		}

		public void SetVariables(float damage)
		{
			_damage = damage;
		}

		void OnParticleCollision(GameObject other)
		{
			hitLogic.HitThisObject(other, _damage);
		}
	}
}
