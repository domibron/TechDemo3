using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public interface IWeaponAmmo
	{
		public string AmmoString { get; }

		public bool HasAmmo();

		public void Reload();

		public void ResetAllAmmo();

		public void StartReducingAmmo();

		public void StopReducingAmmo();
	}
}
