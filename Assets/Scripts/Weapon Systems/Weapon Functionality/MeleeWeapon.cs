using System;
using System.Collections;
using System.Collections.Generic;
using Project.HealthSystems;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class MeleeWeapon : BaseWeapon
	{

		// Privates
		[SerializeField]
		private WeaponProjectileBase _weaponProjectile;

		[SerializeField]
		private WeaponAudioBase _weaponAudio;

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

		public override void AimKeyHeld(bool state)
		{
			// Should Do with animations.
		}

		public override void FireKeyHeld(bool state)
		{
			if (!state) return;

			if (_timeUntilNextAttack <= 0)
			{
				_timeUntilNextAttack = 1f;

				_weaponProjectile.StartFireProjectile(WeaponSO.Damage, WeaponSO.Range);
			}
		}

		public override void ReloadKeyPressed()
		{

			// inspect. or dont.
		}

		public override void SpecialKeyPressed()
		{
			// Other special animtion.
		}

		protected override void SetUpWeapon()
		{
			if (WeaponSO == null)
			{
				throw new NullReferenceException($"HEY! I cannot use nothing for a weapon! please add a {nameof(WeaponSOBase)} to {nameof(WeaponSO)}!");
			}

			_weaponProjectile = GetComponent<WeaponProjectileBase>();
		}

		// protected override void FireKeyUpdate(bool state)
		// {

		// }
	}
}
