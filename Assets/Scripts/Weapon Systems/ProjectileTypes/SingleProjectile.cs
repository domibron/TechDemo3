using System.Collections;
using System.Collections.Generic;
using Project.HealthSystems;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class SingleProjectile : MonoBehaviour, IWeaponProjectile
	{
		void IWeaponProjectile.FireProjectile(float damage, float range)
		{
			if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
			{
				hit.collider.GetComponent<IHealth>()?.DamageHealth(damage);
			}


			Debug.DrawRay(transform.position, transform.forward * range, Color.red, 10);
		}
	}
}
