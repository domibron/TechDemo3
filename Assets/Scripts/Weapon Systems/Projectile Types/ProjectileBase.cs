using System.Collections;
using System.Collections.Generic;
using Project.Gore;
using UnityEngine;

namespace Project.WeaponSystems
{
	public abstract class ProjectileBase : MonoBehaviour, IWeaponProjectile
	{

		public void EndFireProjectile()
		{

		}

		public void StartFireProjectile(float damage, float range)
		{

		}

		public virtual void EndFire()
		{

		}

		public virtual void StartFire(float damage, float range)
		{

		}
	}
}
