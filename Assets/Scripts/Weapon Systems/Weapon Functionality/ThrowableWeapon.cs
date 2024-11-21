using System;
using System.Collections;
using System.Collections.Generic;
using Project.UI.WeaponStats;
using UnityEngine;

namespace Project.WeaponSystems
{
	[RequireComponent(typeof(WeaponAmmoPool), typeof(SpawnThrowableWeaponProjectile))]
	public class ThrowableWeapon : BaseThrowableWeapon
	{

		public override string DisplayName => WeaponSO.name;

		public override string AmmoDisplay => _weaponAmmo != null ? _weaponAmmo.AmmoString : "";


		public float MaxForce = 15f;

		public float MinForce = 1f;

		public float HowFastToReachMax = 3f;

		public float UpArchAmmount = 0.2f;

		private WeaponAmmoPool _weaponAmmo;

		private SpawnThrowableWeaponProjectile _weaponProjectile;

		private bool _pulledThrowable = false;
		private bool _thrown = false;

		private float _throwCharge = 0f;

		private float _timeUntilNextAttack = 0;


		void Start()
		{
			SetUpWeapon();
		}

		void Update()
		{
			if (_pulledThrowable && _throwCharge <= 1f)
			{
				_throwCharge += Time.deltaTime * (1 / HowFastToReachMax);
			}
			else if (!_pulledThrowable)
			{
				_throwCharge = 0;
			}

			if (_timeUntilNextAttack >= 0)
			{
				_timeUntilNextAttack -= Time.deltaTime * WeaponSO.FireRate;
			}
		}

		public override void AimKeyHeld(bool state)
		{
			if (!state)
			{
				_pulledThrowable = false;
				return;
			}

			_pulledThrowable = true;
		}

		void OnDisable()
		{
			_pulledThrowable = false;
			// _thrown = false;

			_weaponAmmo.StopReducingAmmo();

			_throwCharge = 0f;
		}

		void OnEnable()
		{

		}

		public override void FireKeyHeld(bool state)
		{


			if (!state)
			{
				if (_thrown)
				{
					_weaponAmmo.StartReducingAmmo();
					_timeUntilNextAttack = 1f;
					_weaponProjectile.StartThrowProjectile(WeaponSO.Damage, WeaponSO.Range, (transform.forward + (transform.up * UpArchAmmount)) * Mathf.Lerp(MinForce, MaxForce, _throwCharge));
				}

				_thrown = false;
				_weaponAmmo.StopReducingAmmo();

				return;
			}

			if (_thrown || _timeUntilNextAttack > 0 || !_weaponAmmo.HasAmmo())
			{
				return;
			}



			_thrown = true;
		}

		public override void ReloadKeyPressed()
		{

		}

		public override void SpecialKeyPressed()
		{

		}

		protected override void SetUpWeapon()
		{
			if (WeaponSO == null)
			{
				throw new NullReferenceException($"HEY! I cannot use nothing for a weapon! please add a {nameof(WeaponSOBase)} to {nameof(WeaponSO)}!");
			}

			_weaponAmmo = GetComponent<WeaponAmmoPool>();

			_weaponAmmo.ResetAllAmmo();

			_weaponProjectile = GetComponent<SpawnThrowableWeaponProjectile>();
		}

		public override void GKeyHeld(bool state)
		{

		}
	}
}
