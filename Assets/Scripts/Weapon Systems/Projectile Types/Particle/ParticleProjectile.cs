using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class ParticleProjectile : WeaponProjectileBase
	{
		public ParticleSystem WeaponParticleSystem;

		public ParticaleProjectileDamageCode ProjectileDamager;

		void Start()
		{
			WeaponParticleSystem.Stop();
		}

		public override void EndFireProjectile()
		{
			WeaponParticleSystem.Stop();

		}

		public override void StartFireProjectile(float damage, float range)
		{
			ProjectileDamager.SetVariables(damage);

			WeaponParticleSystem.Play();
		}
	}
}
