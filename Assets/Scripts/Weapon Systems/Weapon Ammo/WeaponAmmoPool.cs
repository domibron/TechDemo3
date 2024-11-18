using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class WeaponAmmoPool : MonoBehaviour, IWeaponAmmo
	{
		string IWeaponAmmo.AmmoString => "Ammo:\n" + _currentAmmo;

		public float MaxAmmo = 200;

		public float AmmoReductionWhenFired = 1;

		private float _currentAmmo;

		bool IWeaponAmmo.HasAmmo()
		{
			return _currentAmmo > 0;
		}

		void IWeaponAmmo.Reload()
		{
			// NOPE
		}

		void IWeaponAmmo.ResetAllAmmo()
		{
			_currentAmmo = MaxAmmo;
		}
		void IWeaponAmmo.StartReducingAmmo()
		{
			_currentAmmo -= AmmoReductionWhenFired;
		}

		void IWeaponAmmo.StopReducingAmmo()
		{

		}
	}
}
