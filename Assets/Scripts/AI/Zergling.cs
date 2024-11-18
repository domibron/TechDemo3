using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Project.AI
{
	[RequireComponent(typeof(NavMeshAgent), typeof(CharacterController))]
	public class Zergling : MonoBehaviour
	{

		private NavMeshAgent AIAgent;

		private float health = 100f;

		private Transform PlayerTarget;

		private NavMeshPath path;

		void Start()
		{
			SetUpZergling();


		}

		void Update()
		{


		}

		public void KillZergling()
		{
			Destroy(this.gameObject);
		}

		private void CalculateAIThinking()
		{
			path = new NavMeshPath();

			NavMesh.CalculatePath(transform.position, PlayerTarget.position, 1 << 32, path);

			AIAgent.path = path;
		}


		private void SetUpZergling()
		{
			AIAgent = GetComponent<NavMeshAgent>();

			PlayerTarget = GameObject.FindWithTag("Player").transform;

			InvokeRepeating(nameof(CalculateAIThinking), 0, 1);
		}
	}
}
