using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public interface IWeaponAnimation
	{
		public void Aim(bool state);
		public void Fire(bool state);
		public void Reload();
		public void SpecialAction();
	}
}
