using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class WeaponAmmoTank : MonoBehaviour, IWeaponAmmo
	{
		string IWeaponAmmo.AmmoString => $"Ammo:\n{((_currentAmmo / MaxAmmo) * 100f):F0}%";

		public float MaxAmmo = 100f;

		public float AmmoConsumptionRate = 1f;

		private float _currentAmmo;

		private bool _reduceAmmo = false;

		bool IWeaponAmmo.HasAmmo()
		{
			return _currentAmmo > 0;
		}

		void IWeaponAmmo.Reload()
		{
			throw new System.NotImplementedException();
		}

		void IWeaponAmmo.ResetAllAmmo()
		{
			_currentAmmo = MaxAmmo;
		}

		void IWeaponAmmo.StartReducingAmmo()
		{
			_reduceAmmo = true;
		}

		void IWeaponAmmo.StopReducingAmmo()
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
