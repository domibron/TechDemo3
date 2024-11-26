using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GameManager
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance { get; private set; }

		public LayerMask SpawnNodeLayerMask;

		public float MaxSpawnRadius = 30f;

		public GameObject ZirglingPrefab;

		private Transform _playerTransform;

		private int _spawnAmmount = 0;

		private int _currentRound = 0;

		private int _ammountIncrease = 10;

		private int _currentZirglings = 0;

		private int _zirglingSpawned = 0;

		private bool _playingRound = false;


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

		// Start is called before the first frame update
		void Start()
		{
			_playerTransform = GameObject.Find("Player").transform;


			NextRound();
		}

		// Update is called once per frame
		void Update()
		{
			if (_currentZirglings < 0 && _playingRound)
			{
				_playingRound = false;
				NextRound();
			}
		}

		public void NextRound()
		{
			_currentRound++;
			_zirglingSpawned = 0;
			_playingRound = true;
			_spawnAmmount = _ammountIncrease + (_currentRound * _currentRound);

			BeginSpawning();
		}

		public void BeginSpawning()
		{

			StartCoroutine(SpawnZirglingCorutine());

		}


		IEnumerator SpawnZirglingCorutine()
		{
			while (_playingRound)
			{
				if (_zirglingSpawned <= _spawnAmmount)
				{

					Transform target = SpawnPoints.Instance.GetRandomSpawnPoint();

					if (target != null)
					{

						Instantiate(ZirglingPrefab, target.position, target.rotation);

						_zirglingSpawned++;
						_currentZirglings++;
					}
				}
				yield return new WaitForSeconds(0.5f);
			}
		}
	}
}

