using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class DamageOnTriggerEnter : MonoBehaviour
	{
		[SerializeField]
		private ProjectileHitLogicBase _hitLogic;

		private float _damage;

		void Start()
		{
			_hitLogic = GetComponent<ProjectileHitLogicBase>();
		}

		public void SetUpTrigger(float damage)
		{
			_damage = damage;
		}

		void OnTriggerEnter(Collider other)
		{
			_hitLogic.HitThisObject(other.gameObject, _damage);
		}
	}
}
