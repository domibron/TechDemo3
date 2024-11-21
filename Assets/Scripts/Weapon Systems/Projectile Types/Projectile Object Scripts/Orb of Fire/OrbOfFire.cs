using System.Collections;
using System.Collections.Generic;
using Project.HealthSystems;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class OrbOfFire : MonoBehaviour, IWeaponProjectileObject
	{
		public float ZigZagDelay = 3f;

		public float Speed = 3f;

		public int MaxCreatureToDamageCount = 3;

		public float CoolDown = 1f;

		[SerializeField]
		private ProjectileHitLogicBase _hitLogic;

		private PassTriggerEvents _passTriggerEvents;

		private Vector3 _dir;

		private float _zigZagTimer = 0f;

		private Rigidbody _rigidbody;

		private float _damage;

		private float _range;

		private bool _zigRight = false;

		private List<GameObject> _entities = new List<GameObject>();

		private float _coolDownTimer = 0f;




		void Start()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		void IWeaponProjectileObject.SetUpPrefab(float damage, float range)
		{
			_range = range; _damage = damage; // WHAT THE SIGMA? ONE LINE!

			_passTriggerEvents = GetComponentInChildren<PassTriggerEvents>();
			_passTriggerEvents.OnTriggerEnterEvent += AddHealthObject;
			_passTriggerEvents.OnTriggerExitEvent += RemoveHealthObject;



			transform.rotation = Quaternion.LookRotation(new Vector3(transform.forward.x, 0, transform.forward.z));

			_zigRight = Random.Range(0, 1) >= 0.5f;
			_dir = Quaternion.AngleAxis(45 * (_zigRight ? 1 : -1), transform.up) * transform.forward;

			transform.rotation = Quaternion.LookRotation(_dir);

			_zigZagTimer = ZigZagDelay / 2f;


			Destroy(this.gameObject, _range);
		}

		void OnDisable()
		{
			// make sure to remove event. dont know why, just do.
			_passTriggerEvents.OnTriggerEnterEvent -= AddHealthObject;
			_passTriggerEvents.OnTriggerExitEvent -= RemoveHealthObject;

		}

		void Update()
		{
			if (_coolDownTimer > 0) _coolDownTimer -= Time.deltaTime;
			else
			{

				StartCoroutine(CollectAndDamage());





				_coolDownTimer = CoolDown;
			}

			if (_zigZagTimer > 0)
			{
				_zigZagTimer -= Time.deltaTime;
			}
			else
			{
				ZigZag();
			}


			_rigidbody.velocity = transform.forward * Speed;
		}

		private void ZigZag()
		{
			_zigZagTimer = ZigZagDelay;

			_zigRight = !_zigRight;

			_dir = Quaternion.AngleAxis(90 * (_zigRight ? 1 : -1), transform.up) * transform.forward;

			transform.rotation = Quaternion.LookRotation(_dir);
		}

		void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.layer == StaticData.PLAYER_LAYER || other.gameObject.layer == StaticData.ZIRGLINGS) return;

			_zigZagTimer = ZigZagDelay;

			_zigRight = !_zigRight;

			_dir = Vector3.Reflect(transform.forward * _rigidbody.velocity.magnitude, other.GetContact(0).normal);

			_dir.y = 0;


			transform.rotation = Quaternion.LookRotation(_dir);
		}

		private IEnumerator CollectAndDamage()
		{
			int count = 0;

			List<GameObject> targets = new List<GameObject>();
			if (_entities.Count > 0)
			{


				// IENUMERATOR GOD DAMN IT PLEASE! WHY WHY YOU LIKE THIS! 2 times, 2 frik times
				bool collecting = true;
				while (collecting)
				{


					if (_entities.Count - count <= 0)
					{
						collecting = false;
						break;
					}

					int targ = Random.Range(0, _entities.Count - 1);

					if (!targets.Contains(_entities[targ]))
					{

						targets.Add(_entities[targ]);
						count++;
					}

					if (count >= MaxCreatureToDamageCount)
					{
						collecting = false;
						break;
					}

					yield return null;
				}
			}

			foreach (var target in targets)
			{
				if (target == null) continue;
				print(target.name + "DAMAGEAAGGAEG");
				_hitLogic.HitThisObject(target, _damage);
				yield return null;
			}
		}

		private void AddHealthObject(GameObject targetObject)
		{
			if (targetObject.gameObject.GetComponent<IHealth>() == null) return;

			_entities.Add(targetObject.gameObject);

			// if (_count >= MaxCreatureToDamageCount) return;
			// _hitLogic.HitThisObject(targetObject, _damage);
		}

		private void RemoveHealthObject(GameObject targetObject)
		{
			if (targetObject.gameObject.GetComponent<IHealth>() == null) return;

			_entities.Remove(targetObject);
		}
	}
}
