using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public interface IWeapon
	{
		public string DisplayName { get; }
		public string AmmoDisplay { get; }

		public void FireKeyHeld(bool state);
		public void AimKeyHeld(bool state);
		public void ReloadKeyPressed();
		public void SpecialKeyPressed();

		// public void FireKeyUpdate(bool state);
	}
}
