using System.Collections;
using System.Collections.Generic;
using Project.Gore;
using UnityEngine;
using UnityEngine.AI;
using Project.StatusEffects;

namespace Project.AI
{
	public enum OffMeshLinkMoveMethod
	{
		Teleport,
		NormalSpeed,
		Parabola
	}

	[RequireComponent(typeof(NavMeshAgent))]
	public class Zergling : MonoBehaviour, IFreezable, IStunable, IKnockbackable
	{
		public float NormalUnFreezTime = 1f;

		public float FullFrozenUnFreezTime = 3f;

		public float KnockbackReductionRate = 0.1f;


		float IFreezable.FrozenPercentage => _forzenPercentage;

		private CharacterController _characterController;

		private float _forzenPercentage = 0f;

		private bool _frozenFully = false;

		private IGibs _gibs;

		private NavMeshAgent _aIAgent;

		private Transform _playerTarget;

		private NavMeshPath _path;

		private float _normalSpeed;

		private float _unfreezTime = 0;

		private float _stunTime = 0;

		private Vector3 _velocityForKnockback = Vector3.zero;


		void Start()
		{
			SetUpZergling();
		}




		void Update()
		{


			if (_frozenFully)
			{
				// Do cube
			}

			SortZirglingSpeed();

			if (_unfreezTime > 0)
			{
				_unfreezTime -= Time.deltaTime;
			}
			else if (_forzenPercentage > 0)
			{
				UnFreezeZergling();
			}

			if (_stunTime > 0)
			{
				_stunTime -= Time.deltaTime;
			}

			if (_velocityForKnockback.magnitude >= 0.1f)
			{
				_characterController.Move(_velocityForKnockback * Time.deltaTime);

				_velocityForKnockback += -_velocityForKnockback * KnockbackReductionRate;
			}

		}

		public void KillZergling()
		{
			_gibs.BeginGibs(transform.position);

			Destroy(this.gameObject);
		}

		private void CalculateAIThinking()
		{


			_aIAgent.destination = _playerTarget.position;
		}


		private void SetUpZergling()
		{
			_aIAgent = GetComponent<NavMeshAgent>();

			_characterController = GetComponent<CharacterController>();

			_playerTarget = GameObject.FindWithTag("Player").transform;

			_gibs = GetComponent<IGibs>();

			_normalSpeed = _aIAgent.speed;

			InvokeRepeating(nameof(CalculateAIThinking), 0, 0.1f);
		}

		private void SortZirglingSpeed()
		{

			if (_forzenPercentage > 0 && !_frozenFully && _stunTime <= 0)
			{
				_aIAgent.speed = Mathf.Lerp(_normalSpeed, 0f, _forzenPercentage / 100f);

			}
			else if (_stunTime > 0 || _frozenFully || _velocityForKnockback.magnitude >= 0.1f)
			{
				_aIAgent.speed = 0;
			}
			else
			{
				_aIAgent.speed = _normalSpeed;
			}


		}

		void IFreezable.Freeze(float percentageIncrease)
		{
			_forzenPercentage += percentageIncrease;

			if (_forzenPercentage >= 100f)
			{
				_forzenPercentage = 100f;
			}
			else if (_forzenPercentage < 0)
			{
				_forzenPercentage = 0;
			}

			if (_forzenPercentage >= 100f)
			{
				_unfreezTime = FullFrozenUnFreezTime;
				_frozenFully = true;
			}
			else
			{
				_unfreezTime = NormalUnFreezTime;
			}
		}

		void IFreezable.UnFreeze()
		{
			UnFreezeZergling();

		}

		private void UnFreezeZergling()
		{
			if (_forzenPercentage >= 100f) return;

			_unfreezTime = 0;
			_forzenPercentage = 0f;


			_frozenFully = false;
		}

		void IStunable.Stun(float duration)
		{
			_stunTime = duration;
		}

		void IKnockbackable.Addknockback(Vector3 force)
		{
			_velocityForKnockback = force;
		}
	}
}
