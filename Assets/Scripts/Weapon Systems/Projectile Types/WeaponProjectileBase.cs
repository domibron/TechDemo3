using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	/*
	Yes, I will call raycasts projectiles. I feel like that.
	*/

	public abstract class WeaponProjectileBase : MonoBehaviour
	{
		public abstract void StartFireProjectile(float damage, float range);
		public abstract void EndFireProjectile();
	}
}
