using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public abstract class ReturnableWeaponProjectileBase : MonoBehaviour
	{
		public abstract (Transform, Vector3) StartFireProjectile(float damage, float range);
		public abstract void EndFireProjectile();
	}
}
