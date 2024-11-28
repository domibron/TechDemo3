using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	[RequireComponent(typeof(Rigidbody))]
	public class RocketProjectile : MonoBehaviour, IWeaponProjectileObject
	{
		public GameObject ExplosionPrefab;

		public float LifeTime = 60f;

		public float Force = 100f;

		private float _damage;

		private float _range;

		void Start()
		{
			// I dont like the idea that there can be alot of these flying infinitly.
			Destroy(this.gameObject, LifeTime);
		}

		void OnCollisionEnter(Collision other)
		{
			print("collided");

			GameObject go = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
			go.GetComponent<IWeaponProjectileObject>().SetUpPrefab(_damage, _range);

			Destroy(this.gameObject);
		}

		void IWeaponProjectileObject.SetUpPrefab(float damage, float range)
		{
			_damage = damage;
			_range = range;


			GetComponent<Rigidbody>().AddForce(transform.forward * Force, ForceMode.Impulse);
		}
	}
}
