using System.Collections;
using System.Collections.Generic;
using Project.Gore;
using UnityEngine;
using UnityEngine.AI;

namespace Project.AI
{
	public enum OffMeshLinkMoveMethod
	{
		Teleport,
		NormalSpeed,
		Parabola
	}

	[RequireComponent(typeof(NavMeshAgent))]
	public class Zergling : MonoBehaviour
	{

		private IGibs _gibs;

		private NavMeshAgent _aIAgent;

		private Transform _playerTarget;

		private NavMeshPath _path;


		void Start()
		{
			SetUpZergling();
		}




		void Update()
		{


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

			InvokeRepeating(nameof(CalculateAIThinking), 0, 0.1f);
		}
	}
}
