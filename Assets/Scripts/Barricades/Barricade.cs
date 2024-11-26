using System.Collections;
using System.Collections.Generic;
using Project.AI;
using UnityEngine;

namespace Project.Barricades
{
	public class Barricade : MonoBehaviour
	{
		public GameObject[] Barricades;

		public float PullRate = 2f;

		private int _barricadesLeft = 0;

		private int _count = 0;

		private float _countTime;

		private List<GameObject> _zerglings = new List<GameObject>();

		void Start()
		{
			_barricadesLeft = Barricades.Length;
		}


		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Zirgling"))
			{
				ClearAllMissing();
				if (other.gameObject.GetComponent<Zergling>() == null) return;

				_zerglings.Add(other.gameObject);
				other.gameObject.GetComponent<Zergling>().AtBarricade = (_barricadesLeft > 0 ? true : false);
				_count++;
			}
		}

		void OnTriggerExit(Collider other)
		{
			if (other.gameObject.CompareTag("Zirgling"))
			{
				_count--;
				_zerglings.Remove(other.gameObject);
				ClearAllMissing();


				if (other.gameObject.GetComponent<Zergling>() == null) return;
				other.gameObject.GetComponent<Zergling>().AtBarricade = false;
			}
		}

		void Update()
		{
			if (_count == 0) return;

			if (_countTime > 0)
			{
				_countTime -= Time.deltaTime;
			}
			else if (_barricadesLeft > 0)
			{
				_countTime = PullRate;
				_barricadesLeft--;
				UpdateBarricades();
				ClearAllMissing();

			}

			if (_barricadesLeft <= 0)
			{
				foreach (var zirg in _zerglings)
				{
					if (zirg != null)
					{
						zirg.GetComponent<Zergling>().AtBarricade = false;
					}
				}


				return;
			}
		}

		void ClearAllMissing()
		{
			foreach (var zirg in _zerglings)
			{
				if (zirg == null)
				{
					_zerglings.Remove(zirg);
				}
			}
		}

		void UpdateBarricades()
		{


			for (int i = 0; i < Barricades.Length; i++)
			{
				if (i < _barricadesLeft)
				{
					Barricades[i].SetActive(true);
				}
				else
				{
					Barricades[i].SetActive(false);

				}
			}
		}
	}
}
