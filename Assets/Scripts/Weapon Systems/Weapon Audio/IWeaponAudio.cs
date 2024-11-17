using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public interface IWeaponAudio
	{
		public void Aim();
		public void UnAim();
		public void Fire();
		public void Reload();
		public void SpecialAction();
	}
}
