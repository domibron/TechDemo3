using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public abstract class BaseWeapon : MonoBehaviour, IWeapon
	{
		public abstract string DisplayName { get; }
		public abstract string AmmoDisplay { get; }

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

		void IWeapon.FireKeyUpdate(bool state)
		{
			FireKeyUpdate(state);
		}



		protected abstract void SetUpWeapon();



		protected abstract void AimWeapon();

		protected abstract void FireWeapon();

		protected abstract void ReloadWeapon();

		protected abstract void FireKeyUpdate(bool state);


	}
}
