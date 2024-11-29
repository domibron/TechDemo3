using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	[RequireComponent(typeof(SpawnPysicsWeaponProjectile))]
	public class ChargeBasedWeapon : BaseWeapon
	{
		public override string DisplayName => WeaponSO.name;

		public override string AmmoDisplay => _weaponAmmo != null ? _weaponAmmo.AmmoString : "";

		public float MaxChargedForce = 15f;
		public float MinChargedForce = 1f;

		public float ChargeRate = 1f;

		[SerializeField]
		private SpawnPysicsWeaponProjectile _weaponPysProjectile;

		[SerializeField]
		private WeaponAudioBase _weaponAudio;

		[SerializeField]
		private WeaponAmmoBase _weaponAmmo;

		[SerializeField]
		private WeaponAnimatorBase _weaponAnimator;

		private float _timeUntilNextAttack = 0;

		private bool _charging = false;


		protected bool _isAiming = false;

		private float _chargeTime = 0f;


		private void Start()
		{
			SetUpWeapon();
		}

		private void Update()
		{
			if (_timeUntilNextAttack >= 0)
			{
				_timeUntilNextAttack -= Time.deltaTime * WeaponSO.FireRate;
			}

			if (_charging && _chargeTime < 1f)
			{
				_chargeTime += Time.deltaTime * ChargeRate;
			}
			else if (!_charging)
			{
				_chargeTime = 0f;
			}


		}

		void OnDisable()
		{
			print("Disabled");
			StopFiring();
			StopAllCoroutines();
		}

		private void StopFiring()
		{
			_charging = false;
			if (_weaponPysProjectile != null) _weaponPysProjectile.EndFireProjectile();
			if (_weaponAmmo != null) _weaponAmmo.StopReducingAmmo();

			if (_weaponAnimator != null) _weaponAnimator.Fire(false);
		}

		public override void AimKeyHeld(bool state)
		{
			if (_weaponAnimator != null) _weaponAnimator.Aim(state);


			if (_isAiming != state)
			{
				_isAiming = state;


				if (_weaponAudio != null)
				{
					if (state) _weaponAudio.Aim();
					else _weaponAudio.UnAim();
				}
			}
		}

		public override void FireKeyHeld(bool state)
		{
			if (!state)
			{
				if (_charging)
				{
					_weaponPysProjectile.StartPysProjectile(WeaponSO.Damage, WeaponSO.Range, transform.forward * Mathf.Lerp(MinChargedForce, MaxChargedForce, _chargeTime));
					_weaponAmmo.StartReducingAmmo();
					if (_weaponAnimator != null) _weaponAnimator.Fire(true);
				}

				StopFiring();
				return;
			}

			if (_weaponAmmo != null && !_weaponAmmo.HasAmmo())
			{
				// display - tooltip
				StopFiring();

				return;
			}


			// Fire weapon







			_charging = true;
		}


		protected override void SetUpWeapon()
		{
			if (WeaponSO == null)
			{
				throw new NullReferenceException($"HEY! I cannot use nothing for a weapon! please add a {nameof(WeaponSOBase)} to {nameof(WeaponSO)}!");
			}



			_weaponAudio = GetComponent<WeaponAudio>();

			if (_weaponAudio == null)
			{
				Debug.Log("You can add any scripts that inherit " + nameof(WeaponAudioBase) + " to this to play sounds!");
			}

			_weaponPysProjectile = GetComponent<SpawnPysicsWeaponProjectile>();


			_weaponAmmo = GetComponent<WeaponAmmoBase>();

			if (_weaponAmmo == null)
			{
				Debug.LogWarning("Are you sure you want to use weapon with no " + nameof(WeaponAmmoBase) + "! Do you want ammo? cos you might need it.");
			}

			if (_weaponAmmo != null) _weaponAmmo.ResetAllAmmo();
		}

		public override void SpecialKeyPressed()
		{
			if (_weaponAnimator != null) _weaponAnimator.SpecialAction();
		}


		public override void ReloadKeyPressed()
		{
			if (_weaponAmmo != null) _weaponAmmo.Reload();

			if (_weaponAudio != null)
			{
				_weaponAudio.Reload();
			}
			if (_weaponAnimator != null) _weaponAnimator.Reload();
		}

	}
}
