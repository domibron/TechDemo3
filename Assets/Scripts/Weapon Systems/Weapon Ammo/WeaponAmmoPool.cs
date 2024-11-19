using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class WeaponAmmoPool : WeaponAmmoBase
	{
		public override string AmmoString => "Ammo:\n" + _currentAmmo;

		public float MaxAmmo = 200;

		public float AmmoReductionWhenFired = 1;

		private float _currentAmmo;



		public override bool HasAmmo()
		{
			return _currentAmmo > 0;
		}

		public override void Reload()
		{

		}

		public override void ResetAllAmmo()
		{
			_currentAmmo = MaxAmmo;
		}

		public override void StartReducingAmmo()
		{
			_currentAmmo -= AmmoReductionWhenFired;
		}

		public override void StopReducingAmmo()
		{

		}
	}
}
