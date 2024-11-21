using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class Grenade : MonoBehaviour, IWeaponProjectileObject
	{
		public GameObject ExplosionPrefab;

		public bool ExploadOnContact = false;

		// yes, this will conflict.
		public bool IsSticky = false;

		public float FuseTime = 3f;

		private float _damage;
		private float _range;

		private Rigidbody _rigidbody;


		void IWeaponProjectileObject.SetUpPrefab(float damage, float range)
		{
			_damage = damage;
			_range = range;

			_rigidbody = GetComponent<Rigidbody>();


			StartCoroutine(ExploadAfterSetTime());
		}

		IEnumerator ExploadAfterSetTime()
		{
			yield return new WaitForSeconds(FuseTime);

			Expload();
		}

		void OnCollisionEnter(Collision other)
		{
			if (ExploadOnContact)
			{
				Expload();
			}
			else if (IsSticky)
			{
				_rigidbody.isKinematic = true;
				transform.parent = other.transform;
			}
		}

		private void Expload()
		{
			GameObject go = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
			go.GetComponent<IWeaponProjectileObject>().SetUpPrefab(_damage, _range);

			Destroy(this.gameObject);
		}
	}
}
