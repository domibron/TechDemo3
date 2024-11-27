using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Managers
{
	public class SpawnPoints : MonoBehaviour
	{
		public static SpawnPoints Instance;

		public GameObject[] SpawnPointsGameObjects;

		void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this);
			}
			else
			{
				Instance = this;
			}
		}

		public Transform GetRandomSpawnPoint()
		{
			return SpawnPointsGameObjects[Random.Range(0, SpawnPointsGameObjects.Length - 1)].transform;
		}
	}
}
