using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public interface IWeapon
	{
		public void Fire();
		public void Aim();
		public void RPressed();
	}
}
