using UnityEngine;

namespace Project.WeaponSystems
{
	public abstract class BaseThrowableWeapon : BaseWeapon
	{
		public abstract void GKeyHeld(bool state);
	}
}