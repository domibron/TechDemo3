using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class WeaponAmmoTank : WeaponAmmoBase
	{
		public override string AmmoString => $"Ammo:\n{((_currentAmmo / MaxAmmo) * 100f):F0}%";

		public float MaxAmmo = 100f;

		public float AmmoConsumptionRate = 1f;

		private float _currentAmmo;

		private bool _reduceAmmo = false;

		public override bool HasAmmo()
		{
			return _currentAmmo > 0;
		}

		public override void Reload()
		{
			throw new System.NotImplementedException();
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
		}

		void Update()
		{
			if (_currentAmmo > 0 && _reduceAmmo)
			{
				_currentAmmo -= Time.deltaTime * AmmoConsumptionRate;
			}
		}
	}
}
