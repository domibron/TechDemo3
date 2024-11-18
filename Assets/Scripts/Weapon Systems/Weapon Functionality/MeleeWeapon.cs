using System;
using System.Collections;
using System.Collections.Generic;
using Project.HealthSystems;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class MeleeWeapon : BaseWeapon
	{
		public WeaponSOBase WeaponSO;

		// Privates
		private IWeaponProjectile weaponProjectile;

		private IWeaponAudio weaponAudio;

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

		protected override void AimWeapon(bool state)
		{
			// Should Do with animations.
		}

		protected override void FireWeapon(bool state)
		{
			if (!state) return;

			if (_timeUntilNextAttack <= 0)
			{
				_timeUntilNextAttack = 1f;

				weaponProjectile.StartFireProjectile(WeaponSO.Damage, WeaponSO.Range);
			}
		}

		protected override void ReloadKeyPressed()
		{
			// inspect. or dont.
		}

		protected override void SpecialKeyPressed()
		{
			// Other special animtion.
		}

		protected override void SetUpWeapon()
		{
			if (WeaponSO == null)
			{
				throw new NullReferenceException($"HEY! I cannot use nothing for a weapon! please add a {nameof(WeaponSOBase)} to {nameof(WeaponSO)}!");
			}

			weaponProjectile = GetComponent<IWeaponProjectile>();
		}

		// protected override void FireKeyUpdate(bool state)
		// {

		// }
	}
}
