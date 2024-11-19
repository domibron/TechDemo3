using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class ParticleProjectile : MonoBehaviour, IWeaponProjectile
	{
		public ParticleSystem WeaponParticleSystem;

		public ParticaleProjectileDamageCode ProjectileDamager;

		void Start()
		{
			WeaponParticleSystem.Stop();
		}

		void IWeaponProjectile.EndFireProjectile()
		{
			WeaponParticleSystem.Stop();

		}

		void IWeaponProjectile.StartFireProjectile(float damage, float range)
		{
			ProjectileDamager.SetVariables(damage);

			WeaponParticleSystem.Play();
		}
	}
}
