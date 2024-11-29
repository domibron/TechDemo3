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

		[SerializeField]
		private ProjectileHitLogicBase _hitLogic;

		[SerializeField]
		private WeaponAudioBase _weaponAudio;

		private float _damage;

		private float _radiusAsRange;

		private float _localTime = 0f;

		void Start()
		{
			_hitLogic = GetComponent<ProjectileHitLogicBase>();
			_weaponAudio = GetComponent<WeaponAudioBase>();


			if (_hitLogic == null)
			{
				throw new NullReferenceException("Cannot work with no logic, please add something with " + nameof(ProjectileHitLogicBase) + " to this object!");
			}


		}

		IEnumerator Expand()
		{
			if (!StartImmediately) yield return new WaitForSeconds(Delay);

			if (_weaponAudio != null) _weaponAudio.Fire(true);

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
			_hitLogic.HitThisObject(other.gameObject, _damage);


		}

		void IWeaponProjectileObject.SetUpPrefab(float damage, float range)
		{
			_damage = damage;
			_radiusAsRange = range;

			_weaponAudio.SetUpAudio();
			transform.localScale = Vector3.zero;
			StartCoroutine(Expand());
		}
	}
}
