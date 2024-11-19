using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class AOEExplosion : MonoBehaviour, IWeaponProjectileObject
	{

		public float ExplosionSpeed;

		public bool StartImmediately = true;

		public float Delay = 1f;


		private ProjectileHitLogicBase hitLogic;


		private float _damage;

		private float _radiusAsRange;

		private float _localTime = 0f;

		void Start()
		{
			hitLogic = GetComponent<ProjectileHitLogicBase>();

			if (hitLogic == null)
			{
				throw new NullReferenceException("Cannot work with no logic, please add something with " + nameof(ProjectileHitLogicBase) + " to this object!");
			}


		}

		IEnumerator Expand()
		{
			if (!StartImmediately) yield return new WaitForSeconds(Delay);


			while (_localTime <= 1)
			{
				_localTime += Time.deltaTime * (1 / ExplosionSpeed);

				transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(_radiusAsRange, _radiusAsRange, _radiusAsRange), _localTime);

				yield return null;
			}

			Destroy(this.gameObject);
		}

		void OnTriggerEnter(Collider other)
		{
			hitLogic.HitThisObject(other.gameObject, _damage);
		}

		void IWeaponProjectileObject.SetUpPrefab(float damage, float range)
		{
			_damage = damage;
			_radiusAsRange = range;

			transform.localScale = Vector3.zero;
			StartCoroutine(Expand());
		}
	}
}
