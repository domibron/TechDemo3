using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public abstract class BaseWeapon : MonoBehaviour, IWeapon
	{

		void IWeapon.Aim()
		{
			AimWeapon();
		}

		void IWeapon.Fire()
		{
			FireWeapon();
		}

		void IWeapon.RPressed()
		{
			ReloadWeapon();
		}



		protected abstract void SetUpWeapon();



		protected abstract void AimWeapon();

		protected abstract void FireWeapon();

		protected abstract void ReloadWeapon();

	}
}
