using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class ParticaleProjectileDamageCode : MonoBehaviour
	{
		[SerializeField]
		private ProjectileHitLogicBase hitLogic;

		private float _damage;


		void Start()
		{
			hitLogic = GetComponent<ProjectileHitLogicBase>();

			if (hitLogic == null)
			{
				throw new NullReferenceException("Cannot work with no logic, please add something with " + nameof(ProjectileHitLogicBase) + " to this object!");
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
