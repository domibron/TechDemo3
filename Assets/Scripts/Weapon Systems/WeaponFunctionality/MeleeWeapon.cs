using System.Collections;
using System.Collections.Generic;
using Project.HealthSystems;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class MeleeWeapon : BaseWeapon
	{
		public MeleeWeaponSO WeaponSO;

		// Privates
		private IWeaponProjectile weaponProjectile;

		private float _timeUntilNextAttack = 0;

		public override string DisplayName { get => WeaponSO.name; }
		public override string AmmoDisplay { get => ""; }

		void Start()
		{
			SetUpWeapon();
		}

		void Update()
		{
			if (_timeUntilNextAttack >= 0)
			{
				_timeUntilNextAttack -= Time.deltaTime * WeaponSO.FireRate;
			}
		}

		protected override void AimWeapon()
		{
			// Should Do with animations.
		}

		protected override void FireWeapon()
		{
			if (_timeUntilNextAttack <= 0)
			{
				_timeUntilNextAttack = 1f;

				weaponProjectile.FireProjectile(WeaponSO.Damage, WeaponSO.Range);
			}
		}

		protected override void ReloadWeapon()
		{
			// inspect. or dont.
		}

		protected override void SetUpWeapon()
		{
			weaponProjectile = GetComponent<IWeaponProjectile>();
		}

		protected override void FireKeyUpdate(bool state)
		{

		}
	}
}
