using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	/*
	Yes, I will call raycasts projectiles. I feel like that.
	*/

	public interface IWeaponProjectile
	{
		public void FireProjectile(float damage, float range);
	}
}
