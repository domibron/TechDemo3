using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class ConstantFirableWeapon : BaseWeapon
	{

		public WeaponSOBase WeaponSO;

		public override string DisplayName => WeaponSO.name;

		public override string AmmoDisplay => weaponAmmo.AmmoString;



		public bool Overheat = false;

		[Tooltip("How fast the overheat bar goes up.")]
		public float OverheatTime = 2f;

		[Tooltip("Time before weapin is usable again after overheat.")]
		public float OverheatCoolDownTime = 2f;

		[Tooltip("How fast the overheat bar goes down.")]
		public float CoolDownTime = 2f;


		public bool SpoolUpBeforeFiring = false;

		public float SpoolUpTime = 2f;

		public float SpoolDownTime = 4f;




		private IWeaponProjectile weaponProjectile;

		private IWeaponAudio weaponAudio;

		private IWeaponAmmo weaponAmmo;


		private bool _beginSpooling = false;

		private float _overheatTime = 0;

		private bool _overheated = false;

		private bool _spooled = false;

		private float _spoolTime = 0;


		private float _timeUntilNextAttack = 0;

		private bool _firedShot = false;

		private bool _firing = false;

		private float _fireTime = 0;

		private float _timeAtDisable = 0;



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

			if (_firing)
			{
				_fireTime += Time.deltaTime;


				// _ammoUsageTime += Time.deltaTime * (1f / WeaponSO.AmmoUsageScaleSpeed);

				if (_overheatTime <= 1 && Overheat) _overheatTime += Time.deltaTime * (1f / OverheatTime);
			}
			else
			{
				_fireTime = 0;
				// _ammoUsageTime = 1;

				if (_overheatTime >= 0 && !_overheated) _overheatTime -= Time.deltaTime * (1f / CoolDownTime);
				else if (_overheatTime >= 0 && _overheated) _overheatTime -= Time.deltaTime * (1f / OverheatCoolDownTime);
			}

			// _ammoUsageRate = Mathf.Lerp(WeaponSO.AmmoUsageRate, WeaponSO.MaxAmmoUsageRate, _ammoUsageTime);

			if (_beginSpooling)
			{
				if (_spoolTime <= 1) _spoolTime += Time.deltaTime * (1 / SpoolUpTime);
			}
			else
			{
				if (_spoolTime >= 0) _spoolTime -= Time.deltaTime * (1 / SpoolDownTime);
			}

			if (_spoolTime >= 1 || !SpoolUpBeforeFiring)
			{
				_spooled = true;
			}
			else
			{
				_spooled = false;

			}

			if (_overheatTime <= 0)
			{
				_overheated = false;
			}

		}


		void OnEnable()
		{

			float timeSince = Time.time - _timeAtDisable;

			print(timeSince);

			if (timeSince >= _overheatTime)
				_overheatTime = 0;
			else
				_overheatTime -= timeSince;
		}

		void OnDisable()
		{
			print("Disabled");
			StopFiring();
			StopAllCoroutines();
			_spoolTime = 0f;
			_timeAtDisable = Time.time;
		}

		private void StopFiring()
		{
			_firedShot = false;
			_firing = false;

			if (weaponProjectile != null) weaponProjectile.EndFireProjectile();
			_beginSpooling = false;
			if (weaponAmmo != null) weaponAmmo.StopReducingAmmo();
		}

		protected override void FireWeapon(bool state)
		{


			if (!state || (_overheated && Overheat))
			{
				StopFiring();
				return;
			}


			_beginSpooling = true;



			if (!weaponAmmo.HasAmmo())
			{
				// display - tooltip

				return;
			}

			if (!_spooled)
			{
				return;
			}

			if ((_overheatTime >= 1) && Overheat)
			{
				_overheated = true;
				StopFiring();
				return;
			}

			_firing = true;

			// do cool things

			if (_timeUntilNextAttack <= 0)
			{
				_timeUntilNextAttack = 1f;

				weaponAmmo.StartReducingAmmo();

				FireProjectile();
			}

			_firedShot = true;
		}

		protected override void SetUpWeapon()
		{
			if (WeaponSO == null)
			{
				throw new NullReferenceException($"HEY! I cannot use nothing for a weapon! please add a {nameof(WeaponSOBase)} to {nameof(WeaponSO)}!");
			}

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

			weaponAmmo = GetComponent<IWeaponAmmo>();

			if (weaponAmmo == null)
			{
				throw new NullReferenceException("Cannot use weapon with no " + nameof(IWeaponAmmo) + "! Do you want ammo? cos you need it.");
			}

			weaponAmmo.ResetAllAmmo();
		}

		protected override void AimWeapon(bool state)
		{

		}

		protected override void ReloadKeyPressed()
		{

		}

		protected override void SpecialKeyPressed()
		{

		}



		protected virtual void FireProjectile()
		{
			weaponProjectile.StartFireProjectile(WeaponSO.Damage, WeaponSO.Range);

			if (weaponAudio != null)
			{
				weaponAudio.Fire(_firing);
			}

			print("BANG!");
		}

	}

}
