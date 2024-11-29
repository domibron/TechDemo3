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

		[SerializeField]
		private WeaponAnimatorBase _weaponAnimator;

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
			if (_weaponAnimator != null) _weaponAnimator.Aim(state);
			// play once
			// if (_weaponAudio != null && state) _weaponAudio.Aim();


		}

		public override void FireKeyHeld(bool state)
		{
			if (_weaponAnimator != null) _weaponAnimator.Fire(state);

			if (!state) return;



			if (_timeUntilNextAttack <= 0)
			{
				_timeUntilNextAttack = 1f;
				if (_weaponAudio != null) _weaponAudio.Fire(true);
				_weaponProjectile.StartFireProjectile(WeaponSO.Damage, WeaponSO.Range);
			}
		}

		public override void ReloadKeyPressed()
		{
			if (_weaponAnimator != null) _weaponAnimator.Reload();
			if (_weaponAudio != null) _weaponAudio.Reload();
			// inspect. or dont.
		}

		public override void SpecialKeyPressed()
		{

			if (_weaponAnimator != null) _weaponAnimator.SpecialAction();
			if (_weaponAudio != null) _weaponAudio.SpecialAction();

			// Other special animtion.
		}

		protected override void SetUpWeapon()
		{
			if (WeaponSO == null)
			{
				throw new NullReferenceException($"HEY! I cannot use nothing for a weapon! please add a {nameof(WeaponSOBase)} to {nameof(WeaponSO)}!");
			}

			_weaponProjectile = GetComponent<WeaponProjectileBase>();

			_weaponAnimator = GetComponent<WeaponAnimatorBase>();

			if (_weaponAnimator == null)
			{
				Debug.Log("This weapon class supports weapon animations.");

			}

			_weaponAudio = GetComponent<WeaponAudioBase>();

			if (_weaponAudio == null)
			{
				Debug.Log("You can use audio with this");
			}
		}

		// protected override void FireKeyUpdate(bool state)
		// {

		// }
	}
}
