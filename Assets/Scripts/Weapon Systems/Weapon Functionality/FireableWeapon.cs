using System;
using System.Collections;
using System.Collections.Generic;
using Project.HealthSystems;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class FireableWeapon : BaseWeapon
	{
		public enum WeaponFireMode
		{
			Automatic,
			SemiAutomatic,
			BoltAction,
			Burst,
		}

		public enum WeaponFireSelect
		{
			None,
			SemiAndAutomatic,
			BurstAndAutomatic,
			SemiAndBurst,
			SemiAndBurstAndAutomatic,
		}

		public static float DEFUALT_MAX_FIREABLE_WEAPON_RANGE = 1000;

		public override string DisplayName => WeaponSO.name;

		public override string AmmoDisplay => _weaponAmmo != null ? _weaponAmmo.AmmoString + "\n[" + _currentFireMode + "]" : "";




		/// <summary>
		/// Weather the weapon can change the fire mode.
		/// </summary>
		[Tooltip("If the weapon can switch firemode, and what modes they can switch to.")]
		public WeaponFireSelect FireSelect = WeaponFireSelect.None;

		/// <summary>
		/// How the weapon will fire.
		/// </summary>
		[Tooltip("How firing the weapon behaves.")]
		public WeaponFireMode FireMode = WeaponFireMode.Automatic;

		/// <summary>
		/// Only used when FireSelect is set to WeaponFireMode.Burst. How many projectiles are fired.
		/// </summary>
		[Tooltip("Only used when " + nameof(WeaponFireMode) + " is set to " + nameof(WeaponFireMode.Burst) + ". How many projectiles are fired.")]
		public int BurstAmmount = 3;

		// Privates
		[SerializeField]
		private WeaponProjectileBase _weaponProjectile;

		[SerializeField]
		private WeaponAudioBase _weaponAudio;

		[SerializeField]
		private WeaponAmmoBase _weaponAmmo;



		private WeaponFireMode _currentFireMode;

		private float _timeUntilNextAttack = 0;

		private bool _firedShot = false;

		private bool _firingBurst = false;

		private Coroutine _burstCoroutine;




		protected bool _isAiming = false;


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


		}

		void OnDisable()
		{
			print("Disabled");
			StopFiring();
			StopAllCoroutines();
		}

		private void StopFiring()
		{
			_firedShot = false;
			if (_weaponProjectile != null) _weaponProjectile.EndFireProjectile();
			if (_weaponAmmo != null) _weaponAmmo.StopReducingAmmo();
		}

		public override void AimKeyHeld(bool state)
		{
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
				StopFiring();
				return;
			}

			if (_weaponAmmo != null && !_weaponAmmo.HasAmmo())
			{
				// display - tooltip
				StopFiring();

				return;
			}

			CheckAndFireAutomatic();

			CheckAndFireSemiAutomatic();

			CheckAndFireBolt();

			CheckAndFireBurst();






			_firedShot = true;
		}

		private void CheckAndFireAutomatic()
		{
			if (_timeUntilNextAttack <= 0 && _currentFireMode == WeaponFireMode.Automatic)
			{
				_timeUntilNextAttack = 1f;

				if (_weaponAmmo != null) _weaponAmmo.StartReducingAmmo();

				FireWeaponProjectile();

			}
		}

		private void CheckAndFireSemiAutomatic()
		{
			if (_timeUntilNextAttack <= 0 && _currentFireMode == WeaponFireMode.SemiAutomatic && !_firedShot)
			{
				_timeUntilNextAttack = 1f;

				if (_weaponAmmo != null) _weaponAmmo.StartReducingAmmo();


				FireWeaponProjectile();

			}
		}

		private void CheckAndFireBolt()
		{
			if (_timeUntilNextAttack <= 0 && _currentFireMode == WeaponFireMode.BoltAction && !_firedShot)
			{
				_timeUntilNextAttack = 1f;

				if (_weaponAmmo != null) _weaponAmmo.StartReducingAmmo();


				FireWeaponProjectile();

			}
		}

		private void CheckAndFireBurst()
		{
			if (_timeUntilNextAttack <= 0 && _currentFireMode == WeaponFireMode.Burst && !_firedShot)
			{
				_timeUntilNextAttack = 1f;

				if (!_firingBurst) _burstCoroutine = StartCoroutine(FireBurst());
			}
		}

		private IEnumerator FireBurst()
		{
			_firingBurst = true;

			for (int i = 0; i < BurstAmmount; i++)
			{
				if (_weaponAmmo != null && !_weaponAmmo.HasAmmo()) break;

				if (_weaponAmmo != null) _weaponAmmo.StartReducingAmmo();

				FireWeaponProjectile();

				yield return new WaitForSeconds(1 / WeaponSO.FireRate);
			}

			_firingBurst = false;
		}

		private void FireWeaponProjectile()
		{
			_weaponProjectile.StartFireProjectile(WeaponSO.Damage, WeaponSO.Range);



			if (_weaponAudio != null)
			{
				_weaponAudio.Fire(_firedShot);
			}

			print("BANG!");
		}



		protected override void SetUpWeapon()
		{
			if (WeaponSO == null)
			{
				throw new NullReferenceException($"HEY! I cannot use nothing for a weapon! please add a {nameof(WeaponSOBase)} to {nameof(WeaponSO)}!");
			}

			_currentFireMode = FireMode;



			_weaponAudio = GetComponent<WeaponAudio>();

			if (_weaponAudio == null)
			{
				Debug.Log("You can add any scripts that inherit " + nameof(WeaponAudioBase) + " to this to play sounds!");
			}

			_weaponProjectile = GetComponent<WeaponProjectileBase>();

			if (_weaponProjectile == null)
			{
				throw new NullReferenceException("Cannot use weapon with not projectile script! Please add one that has the " + nameof(WeaponProjectileBase) + " interface attached.");
			}

			_weaponAmmo = GetComponent<WeaponAmmoBase>();

			if (_weaponAmmo == null)
			{
				Debug.LogWarning("Are you sure you want to use weapon with no " + nameof(WeaponAmmoBase) + "! Do you want ammo? cos you might need it.");
			}

			if (_weaponAmmo != null) _weaponAmmo.ResetAllAmmo();
		}

		public override void SpecialKeyPressed()
		{
			ChangeWeaponFireMode();

			if (_weaponAudio != null)
			{
				_weaponAudio.SpecialAction();
			}
		}


		private void ChangeWeaponFireMode()
		{
			if (FireSelect == WeaponFireSelect.None)
			{
				return;
			}
			else if (FireSelect == WeaponFireSelect.SemiAndAutomatic)
			{
				if (_currentFireMode == WeaponFireMode.Automatic)
				{
					_currentFireMode = WeaponFireMode.SemiAutomatic;
				}
				else if (_currentFireMode == WeaponFireMode.SemiAutomatic)
				{
					_currentFireMode = WeaponFireMode.Automatic;
				}
			}
			else if (FireSelect == WeaponFireSelect.SemiAndBurst)
			{
				if (_currentFireMode == WeaponFireMode.Burst)
				{
					_currentFireMode = WeaponFireMode.SemiAutomatic;
				}
				else if (_currentFireMode == WeaponFireMode.SemiAutomatic)
				{
					_currentFireMode = WeaponFireMode.Burst;
				}
			}
			else if (FireSelect == WeaponFireSelect.BurstAndAutomatic)
			{
				if (_currentFireMode == WeaponFireMode.Automatic)
				{
					_currentFireMode = WeaponFireMode.Burst;
				}
				else if (_currentFireMode == WeaponFireMode.Burst)
				{
					_currentFireMode = WeaponFireMode.Automatic;
				}
			}
			else if (FireSelect == WeaponFireSelect.SemiAndBurstAndAutomatic)
			{
				if (_currentFireMode == WeaponFireMode.Automatic)
				{
					_currentFireMode = WeaponFireMode.Burst;
				}
				else if (_currentFireMode == WeaponFireMode.Burst)
				{
					_currentFireMode = WeaponFireMode.SemiAutomatic;
				}
				else if (_currentFireMode == WeaponFireMode.SemiAutomatic)
				{
					_currentFireMode = WeaponFireMode.Automatic;
				}
			}
		}

		public override void ReloadKeyPressed()
		{
			if (_weaponAmmo != null) _weaponAmmo.Reload();

			if (_weaponAudio != null)
			{
				_weaponAudio.Reload();
			}
		}
	}
}
