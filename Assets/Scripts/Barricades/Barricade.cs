using System;
using System.Collections;
using System.Collections.Generic;
using Project.AI;
using UnityEngine;

namespace Project.Barricades
{
	public class Barricade : MonoBehaviour, IBarricade
	{
		public GameObject[] Barricades;

		public float PullRate = 2f;

		private int _barricadesLeft = 0;

		private int _count = 0;

		private float _countTime;

		private List<Transform> _zerglings = new List<Transform>();

		void Start()
		{
			_barricadesLeft = Barricades.Length;
		}


		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Zirgling"))
			{
				try
				{
					ClearAllMissing();
				}
				catch (Exception ex)
				{
					Debug.Log("You're killing them too fast!\n" + ex.Message);
				}
				if (other.gameObject.GetComponent<Zergling>() == null) return;

				_zerglings.Add(other.transform);
				other.gameObject.GetComponent<Zergling>().AtBarricade = (_barricadesLeft > 0 ? true : false);
				other.gameObject.GetComponent<Zergling>().BarriacdeTransfom = transform;
				_count++;
			}
		}

		void OnTriggerExit(Collider other)
		{
			if (other.gameObject.CompareTag("Zirgling"))
			{

				_count--;

				_zerglings.Remove(other.transform);
				try
				{
					ClearAllMissing();
				}
				catch (Exception ex)
				{
					Debug.Log("You're killing them too fast!\n" + ex.Message);
				}


				if (other.gameObject.GetComponent<Zergling>() == null) return;
				other.gameObject.GetComponent<Zergling>().AtBarricade = false;
				other.gameObject.GetComponent<Zergling>().BarriacdeTransfom = null;

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
				try
				{
					ClearAllMissing();
				}
				catch (Exception ex)
				{
					Debug.Log("You're killing them too fast!\n" + ex.Message);
				}

			}

			if (_barricadesLeft <= 0)
			{
				foreach (var zirg in _zerglings)
				{
					if (zirg != null)
					{
						zirg.GetComponent<Zergling>().AtBarricade = false;
						zirg.GetComponent<Zergling>().BarriacdeTransfom = null;

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

		void IBarricade.RemoveZirgling(Transform transform)
		{


			if (_zerglings.Remove(transform))
			{
				_count--;


			}
		}
	}
}
