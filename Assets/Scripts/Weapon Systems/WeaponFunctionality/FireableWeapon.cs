using System;
using System.Collections;
using System.Collections.Generic;
using Project.HealthSystems;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class FireableWeapon : BaseWeapon
	{
		public static float DEFUALT_MAX_FIREABLE_WEAPON_RANGE = 1000;

		public FireableWeaponSO WeaponSO;

		public override string DisplayName => WeaponSO.name;

		public override string AmmoDisplay => _ammoDisplayString;

		// Privates
		private IWeaponProjectile weaponProjectile;

		private float _timeUntilNextAttack = 0;

		private bool _firedShot = false;

		private bool _firingBirst = false;

		private Coroutine _birstCoroutine;

		private int _currentAmmoInWeapon;

		private int _currentAmmoPool;

		private string _ammoDisplayString;


		private bool _isReloading = false;


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

		}

		protected override void FireWeapon()
		{

			if (_currentAmmoInWeapon <= 0)
			{
				// display - tooltip

				return;
			}

			if (_timeUntilNextAttack <= 0 && WeaponSO.FireMode == WeaponFireMode.Automatic)
			{
				_timeUntilNextAttack = 1f;

				_currentAmmoInWeapon--;

				FireWeaponProjectile();

			}

			// For now, Bolt Action will do the same as a semi auto, just decrease the firerate.
			else if (_timeUntilNextAttack <= 0 && (WeaponSO.FireMode == WeaponFireMode.SemiAutomatic || WeaponSO.FireMode == WeaponFireMode.BoltAction) && !_firedShot)
			{
				_timeUntilNextAttack = 1f;

				_currentAmmoInWeapon--;

				FireWeaponProjectile();

			}

			else if (_timeUntilNextAttack <= 0 && WeaponSO.FireMode == WeaponFireMode.Burst && !_firedShot)
			{
				_timeUntilNextAttack = 1f;

				if (!_firingBirst) _birstCoroutine = StartCoroutine(FireBirst());
			}


			_firedShot = true;
		}

		protected virtual IEnumerator FireBirst()
		{
			_firingBirst = true;

			for (int i = 0; i < WeaponSO.BurstAmmount; i++)
			{
				if (_currentAmmoInWeapon <= 0) break;

				_currentAmmoInWeapon--;

				FireWeaponProjectile();

				yield return new WaitForSeconds(1 / WeaponSO.FireRate);
			}

			_firingBirst = false;
		}

		protected virtual void FireWeaponProjectile()
		{
			weaponProjectile.FireProjectile(WeaponSO.Damage, DEFUALT_MAX_FIREABLE_WEAPON_RANGE);

			UpdateAmmoDisplay();

			print("BANG!");
		}

		protected override void ReloadWeapon()
		{
			if (_currentAmmoPool >= WeaponSO.MagazineSize)
			{

				int ammountToTake = (WeaponSO.MagazineSize) - _currentAmmoInWeapon;

				// We add one because we can chamber a round. we dont want to lose extra ammo either, because this is not a realistic game.
				if (_currentAmmoInWeapon > 0) ammountToTake++;

				_currentAmmoInWeapon += ammountToTake;

				_currentAmmoPool -= ammountToTake;
			}
			else
			{
				_currentAmmoInWeapon = _currentAmmoPool;
				_currentAmmoPool = 0;
			}

			UpdateAmmoDisplay();
		}

		protected override void SetUpWeapon()
		{
			_currentAmmoInWeapon = WeaponSO.MagazineSize;

			_currentAmmoPool = WeaponSO.MaxAmmo;

			UpdateAmmoDisplay();

			weaponProjectile = GetComponent<IWeaponProjectile>();

			if (weaponProjectile == null)
			{
				throw new NullReferenceException("Cannot use weapon with not projectile script! Please add one that has the " + nameof(IWeaponProjectile) + " interface attached.");
			}
		}

		protected virtual void UpdateAmmoDisplay()
		{
			if (_isReloading) _ammoDisplayString = $"Ammo:\n[Reloading]";
			else _ammoDisplayString = $"Ammo:\n{_currentAmmoInWeapon} / {_currentAmmoPool}";
		}

		protected virtual void ChangeWeaponFireMode()
		{

		}

		protected override void FireKeyUpdate(bool state)
		{
			if (!state) _firedShot = false;
		}
	}
}
