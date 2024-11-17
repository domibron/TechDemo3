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

		private WeaponFireMode currentFireMode;

		private IWeaponAudio weaponAudio;

		private float _timeUntilNextAttack = 0;

		private bool _firedShot = false;

		private bool _firingBurst = false;

		private Coroutine _burstCoroutine;

		private int _currentAmmoInWeapon;

		private int _currentAmmoPool;

		private string _ammoDisplayString;


		private bool _isReloading = false;

		private bool _isAiming = false;


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
			if (_isAiming != state)
			{
				_isAiming = state;


				if (weaponAudio != null)
				{
					if (state) weaponAudio.Aim();
					else weaponAudio.UnAim();
				}
			}
		}

		protected override void FireWeapon(bool state)
		{
			if (!state)
			{
				_firedShot = false;
				return;
			}

			if (_currentAmmoInWeapon <= 0)
			{
				// display - tooltip

				return;
			}

			CheckAndFireAutomatic();

			CheckAndFireSemiAutomatic();

			CheckAndFireBolt();

			CheckAndFireBurst();






			_firedShot = true;
		}

		protected virtual void CheckAndFireAutomatic()
		{
			if (_timeUntilNextAttack <= 0 && currentFireMode == WeaponFireMode.Automatic)
			{
				_timeUntilNextAttack = 1f;

				_currentAmmoInWeapon--;

				FireWeaponProjectile();

			}
		}

		protected virtual void CheckAndFireSemiAutomatic()
		{
			if (_timeUntilNextAttack <= 0 && currentFireMode == WeaponFireMode.SemiAutomatic && !_firedShot)
			{
				_timeUntilNextAttack = 1f;

				_currentAmmoInWeapon--;

				FireWeaponProjectile();

			}
		}

		protected virtual void CheckAndFireBolt()
		{
			if (_timeUntilNextAttack <= 0 && currentFireMode == WeaponFireMode.BoltAction && !_firedShot)
			{
				_timeUntilNextAttack = 1f;

				_currentAmmoInWeapon--;

				FireWeaponProjectile();

			}
		}

		protected virtual void CheckAndFireBurst()
		{
			if (_timeUntilNextAttack <= 0 && currentFireMode == WeaponFireMode.Burst && !_firedShot)
			{
				_timeUntilNextAttack = 1f;

				if (!_firingBurst) _burstCoroutine = StartCoroutine(FireBurst());
			}
		}

		protected virtual IEnumerator FireBurst()
		{
			_firingBurst = true;

			for (int i = 0; i < WeaponSO.BurstAmmount; i++)
			{
				if (_currentAmmoInWeapon <= 0) break;

				_currentAmmoInWeapon--;

				FireWeaponProjectile();

				yield return new WaitForSeconds(1 / WeaponSO.FireRate);
			}

			_firingBurst = false;
		}

		protected virtual void FireWeaponProjectile()
		{
			weaponProjectile.FireProjectile(WeaponSO.Damage, DEFUALT_MAX_FIREABLE_WEAPON_RANGE);

			UpdateAmmoDisplay();

			if (weaponAudio != null)
			{
				weaponAudio.Fire();
			}

			print("BANG!");
		}

		protected override void ReloadKeyPressed()
		{
			if (_isReloading || _currentAmmoInWeapon >= WeaponSO.MagazineSize + 1) return;

			if (weaponAudio != null)
			{
				weaponAudio.Reload();
			}

			StartCoroutine(ReloadOverTime());
		}

		protected virtual IEnumerator ReloadOverTime()
		{
			_isReloading = true;

			UpdateAmmoDisplay();

			yield return new WaitForSeconds(WeaponSO.ReloadSpeed);

			if (_currentAmmoPool >= WeaponSO.MagazineSize)
			{
				// We add one because we can chamber a round. we dont want to lose extra ammo either, because this is not a realistic game.
				int ammountToTake = (WeaponSO.MagazineSize + (_currentAmmoInWeapon > 0 ? 1 : 0)) - _currentAmmoInWeapon;

				// if (_currentAmmoInWeapon > 0) ammountToTake++;

				_currentAmmoInWeapon += ammountToTake;

				_currentAmmoPool -= ammountToTake;
			}
			else
			{
				_currentAmmoInWeapon = _currentAmmoPool;
				_currentAmmoPool = 0;
			}

			_isReloading = false;

			UpdateAmmoDisplay();
		}

		protected override void SetUpWeapon()
		{
			_currentAmmoInWeapon = WeaponSO.MagazineSize;

			_currentAmmoPool = WeaponSO.MaxAmmo;

			currentFireMode = WeaponSO.FireMode;

			UpdateAmmoDisplay();


			weaponAudio = GetComponent<IWeaponAudio>();

			if (weaponAudio == null)
			{
				Debug.Log("You can add any scripts that inherit " + nameof(IWeaponAudio) + " to this to play sounds!");
			}

			weaponProjectile = GetComponent<IWeaponProjectile>();

			if (weaponProjectile == null)
			{
				throw new NullReferenceException("Cannot use weapon with not projectile script! Please add one that has the " + nameof(IWeaponProjectile) + " interface attached.");
			}
		}

		protected virtual void UpdateAmmoDisplay()
		{
			string convertFireModeIntoFunIcons = "";

			if (currentFireMode == WeaponFireMode.Automatic)
			{
				convertFireModeIntoFunIcons = "AUTO";
			}
			else if (currentFireMode == WeaponFireMode.SemiAutomatic)
			{
				convertFireModeIntoFunIcons = "SEMI";
			}
			else if (currentFireMode == WeaponFireMode.Burst)
			{
				convertFireModeIntoFunIcons = "BURST";
			}
			else if (currentFireMode == WeaponFireMode.BoltAction)
			{
				convertFireModeIntoFunIcons = "BOLT";
			}

			if (_isReloading) _ammoDisplayString = $"Ammo:\n[Reloading]\n[{convertFireModeIntoFunIcons}]";
			else _ammoDisplayString = $"Ammo:\n{_currentAmmoInWeapon} / {_currentAmmoPool}\n[{convertFireModeIntoFunIcons}]";
		}

		protected override void SpecialKeyPressed()
		{
			ChangeWeaponFireMode();

			if (weaponAudio != null)
			{
				weaponAudio.SpecialAction();
			}
		}

		protected virtual void ChangeWeaponFireMode()
		{
			if (WeaponSO.FireSelect == WeaponFireSelect.None)
			{
				return;
			}
			else if (WeaponSO.FireSelect == WeaponFireSelect.SemiAndAutomatic)
			{
				if (currentFireMode == WeaponFireMode.Automatic)
				{
					currentFireMode = WeaponFireMode.SemiAutomatic;
				}
				else if (currentFireMode == WeaponFireMode.SemiAutomatic)
				{
					currentFireMode = WeaponFireMode.Automatic;
				}
			}
			else if (WeaponSO.FireSelect == WeaponFireSelect.SemiAndBurst)
			{
				if (currentFireMode == WeaponFireMode.Burst)
				{
					currentFireMode = WeaponFireMode.SemiAutomatic;
				}
				else if (currentFireMode == WeaponFireMode.SemiAutomatic)
				{
					currentFireMode = WeaponFireMode.Burst;
				}
			}
			else if (WeaponSO.FireSelect == WeaponFireSelect.BurstAndAutomatic)
			{
				if (currentFireMode == WeaponFireMode.Automatic)
				{
					currentFireMode = WeaponFireMode.Burst;
				}
				else if (currentFireMode == WeaponFireMode.Burst)
				{
					currentFireMode = WeaponFireMode.Automatic;
				}
			}
			else if (WeaponSO.FireSelect == WeaponFireSelect.SemiAndBurstAndAutomatic)
			{
				if (currentFireMode == WeaponFireMode.Automatic)
				{
					currentFireMode = WeaponFireMode.Burst;
				}
				else if (currentFireMode == WeaponFireMode.Burst)
				{
					currentFireMode = WeaponFireMode.SemiAutomatic;
				}
				else if (currentFireMode == WeaponFireMode.SemiAutomatic)
				{
					currentFireMode = WeaponFireMode.Automatic;
				}
			}

			UpdateAmmoDisplay();
		}

		// protected override void FireKeyUpdate(bool state)
		// {
		// 	if (!state) _firedShot = false;
		// }
	}
}
