using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public interface IWeapon
	{
		public string DisplayName { get; }
		public string AmmoDisplay { get; }

		public void Fire();
		public void Aim();
		public void RPressed();

		public void FireKeyUpdate(bool state);
	}
}
