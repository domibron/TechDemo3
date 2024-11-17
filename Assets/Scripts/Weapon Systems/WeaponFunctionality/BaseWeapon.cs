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

		void IWeapon.AimKeyHeld(bool state)
		{
			AimWeapon(state);
		}

		void IWeapon.FireKeyHeld(bool state)
		{
			FireWeapon(state);
		}

		void IWeapon.ReloadKeyPressed()
		{
			ReloadKeyPressed();
		}

		void IWeapon.SpecialKeyPressed()
		{
			SpecialKeyPressed();
		}

		// void IWeapon.FireKeyUpdate(bool state)
		// {
		// 	FireKeyUpdate(state);
		// }



		protected abstract void SetUpWeapon();



		protected abstract void AimWeapon(bool state);

		protected abstract void FireWeapon(bool state);

		protected abstract void ReloadKeyPressed();

		protected abstract void SpecialKeyPressed();

		// protected abstract void FireKeyUpdate(bool state);


	}
}
