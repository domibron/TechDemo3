using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	[RequireComponent(typeof(Rigidbody))]
	public class SawbladeProjectile : MonoBehaviour, IWeaponProjectileObject
	{
		private int _maxHitsBeforeDesturction;

		// public bool GoThoughZerglings = true;

		public float Speed = 10f;

		private ProjectileHitLogicBase hitLogic;

		private Rigidbody _rigidbody;

		private Vector3 _direction = Vector3.forward;

		private float _damage;


		void Start()
		{
			_rigidbody = GetComponent<Rigidbody>();

			_direction = transform.forward;

			hitLogic = GetComponent<ProjectileHitLogicBase>();

			if (hitLogic == null)
			{
				throw new NullReferenceException("Cannot work with no logic, please add something with " + nameof(ProjectileHitLogicBase) + " to this object!");
			}
		}

		void Update()
		{
			if (_rigidbody.velocity.magnitude < (_direction.normalized * Speed).magnitude) _rigidbody.AddForce((_direction.normalized * Speed) - _rigidbody.velocity, ForceMode.Force);


		}

		void IWeaponProjectileObject.SetUpPrefab(float damage, float range)
		{
			_damage = damage;
			_maxHitsBeforeDesturction = (int)range;
			// we dont care about range. might make range as hits before removal.
		}

		void OnCollisionEnter(Collision other)
		{
			hitLogic.HitThisObject(other.gameObject, _damage);

			_maxHitsBeforeDesturction--;

			if (_maxHitsBeforeDesturction <= 0) Destroy(this.gameObject);

			// if (other.gameObject.layer == StaticData.ZIRGLINGS && !GoThoughZerglings) return;




			// if (Physics.Raycast(transform.position, transform.forward * 999f, out RaycastHit hit, 2f, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS_BUT_WITH_PLAYER))
			// {
			// print(other.gameObject.activeSelf);

			// if (other.gameObject == null || !other.gameObject.activeSelf) return;

			_direction = Vector3.Reflect(transform.forward * _rigidbody.velocity.magnitude, other.GetContact(0).normal);

			_rigidbody.velocity = _direction.normalized * other.relativeVelocity.magnitude;

			transform.rotation = Quaternion.LookRotation(_direction);

			// }

		}
	}
}
