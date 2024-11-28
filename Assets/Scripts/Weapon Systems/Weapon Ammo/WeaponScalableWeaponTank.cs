using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class WeaponScalableWeaponTank : WeaponAmmoBase
	{
		public override string AmmoString => $"Ammo:\n{((_currentAmmo / MaxAmmo) * 100f):F0}%";

		public float MaxAmmo = 100f;

		public float AmmoConsumptionRate = 1f;

		public float AmmoConsumptionRateIncreasseRate = 1f;

		private float _currentAmmo;

		private bool _reduceAmmo = false;

		private float _ammoIncreasseRate = 1f;


		public override bool HasAmmo()
		{
			return _currentAmmo > 0;
		}

		public override bool Reload()
		{
			return false;
		}

		public override void ResetAllAmmo()
		{
			_currentAmmo = MaxAmmo;
		}

		public override void StartReducingAmmo()
		{
			_reduceAmmo = true;
		}

		public override void StopReducingAmmo()
		{
			_reduceAmmo = false;
			_ammoIncreasseRate = 1f;
		}

		void Update()
		{
			if (_currentAmmo > 0 && _reduceAmmo)
			{
				_ammoIncreasseRate += Time.deltaTime * AmmoConsumptionRateIncreasseRate;

				_currentAmmo -= Time.deltaTime * AmmoConsumptionRate * _ammoIncreasseRate;
			}
		}
	}
}
