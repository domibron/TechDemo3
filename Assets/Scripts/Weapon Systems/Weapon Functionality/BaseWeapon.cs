using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public abstract class BaseWeapon : MonoBehaviour
	{
		public abstract string DisplayName { get; }
		public abstract string AmmoDisplay { get; }

		public abstract void AimKeyHeld(bool state);

		public abstract void FireKeyHeld(bool state);

		public abstract void ReloadKeyPressed();

		public abstract void SpecialKeyPressed();

		protected abstract void SetUpWeapon();




	}
}
