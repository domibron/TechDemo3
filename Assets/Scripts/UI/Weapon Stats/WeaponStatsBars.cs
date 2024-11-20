using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.WeaponStats
{
	public class WeaponStatsBars : MonoBehaviour
	{
		public static WeaponStatsBars Instance { get; private set; }

		public float OverheatFillAmmount = 0;

		public bool OverheatIsEnabled = false;

		[SerializeField]
		private Image _overheatBar;

		public float SpoolFillAmmount = 0;

		public bool SpoolIsEnabled = false;

		[SerializeField]
		private Image _spoolBar;

		void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this.gameObject);
			}
			else
			{
				Instance = this;
			}
		}

		void Update()
		{
			_overheatBar.enabled = OverheatIsEnabled;
			_overheatBar.fillAmount = OverheatFillAmmount;

			_spoolBar.enabled = SpoolIsEnabled;
			_spoolBar.fillAmount = SpoolFillAmmount;
		}
	}
}
