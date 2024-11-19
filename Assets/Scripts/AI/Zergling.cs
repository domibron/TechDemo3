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
	public class Zergling : MonoBehaviour, IFreezable
	{
		public float NormalUnFreezTime = 1f;

		public float FullFrozenUnFreezTime = 3f;



		float IFreezable.FrozenPercentage => _forzonPercentage;

		private float _forzonPercentage = 0f;

		private bool _frozenFully = false;

		private IGibs _gibs;

		private NavMeshAgent _aIAgent;

		private Transform _playerTarget;

		private NavMeshPath _path;

		private float _maxSpeed;

		private float _unfreezTime = 0;


		void Start()
		{
			SetUpZergling();
		}




		void Update()
		{
			if (_forzonPercentage >= 100f)
			{
				_frozenFully = true;
			}

			if (_frozenFully)
			{
				// Do cube
			}

			SortZirglingSpeed();

			if (_unfreezTime > 0)
			{
				_unfreezTime -= Time.deltaTime;
			}
			else if (_forzonPercentage > 0)
			{
				UnFreezeZergling();
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

			_playerTarget = GameObject.FindWithTag("Player").transform;

			_gibs = GetComponent<IGibs>();

			_maxSpeed = _aIAgent.speed;

			InvokeRepeating(nameof(CalculateAIThinking), 0, 0.1f);
		}

		private void SortZirglingSpeed()
		{
			_aIAgent.speed = Mathf.Lerp(_maxSpeed, 0f, _forzonPercentage / 100f);
		}

		void IFreezable.Freeze(float percentageIncrease)
		{
			_forzonPercentage += percentageIncrease;

			if (_forzonPercentage >= 100f)
			{
				_forzonPercentage = 100f;
			}
			else if (_forzonPercentage < 0)
			{
				_forzonPercentage = 0;
			}

			if (_forzonPercentage >= 100f)
			{
				_unfreezTime = FullFrozenUnFreezTime;
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
			_unfreezTime = 0;
			_forzonPercentage = 0f;

		}
	}
}
