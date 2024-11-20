using System;
using System.Collections;
using System.Collections.Generic;
using Project.UI.WeaponStats;
using UnityEngine;
using UnityEngine.UI;

namespace Project.WeaponSystems
{
	public class ConstantFirableWeapon : BaseWeapon
	{

		public WeaponSOBase WeaponSO;

		public override string DisplayName => WeaponSO.name;

		public override string AmmoDisplay => weaponAmmo != null ? weaponAmmo.AmmoString : "";



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


		public bool SingleFire = false;


		private WeaponProjectileBase weaponProjectile;

		[SerializeField]
		private WeaponAudioBase weaponAudio;

		[SerializeField]
		private WeaponAmmoBase weaponAmmo;


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
				else if (_overheatTime >= 0 && _overheated)
				{
					_overheatTime -= Time.deltaTime * (1f / OverheatCoolDownTime);
				}
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

			if (SpoolUpBeforeFiring)
			{
				if (!WeaponStatsBars.Instance.SpoolIsEnabled) WeaponStatsBars.Instance.SpoolIsEnabled = true;


				if (!_spooled)
					WeaponStatsBars.Instance.SpoolFillAmmount = _spoolTime;
				else
					WeaponStatsBars.Instance.SpoolFillAmmount = (Mathf.Sin((Time.time - _timeAtDisable) * 30f) + 1 > 1.3f) ? 1 : 0;

			}

			if (_overheatTime <= 0)
			{
				_overheated = false;
			}

			if (Overheat)
			{
				if (!WeaponStatsBars.Instance.OverheatIsEnabled) WeaponStatsBars.Instance.OverheatIsEnabled = true;
				if (!_overheated)
					WeaponStatsBars.Instance.OverheatFillAmmount = _overheatTime;
				else
					WeaponStatsBars.Instance.OverheatFillAmmount = (Mathf.Sin((Time.time - _timeAtDisable) * 30f) + 1 > 1f) ? _overheatTime : 0;
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

			WeaponStatsBars.Instance.OverheatIsEnabled = Overheat;
			WeaponStatsBars.Instance.OverheatFillAmmount = 0;

			WeaponStatsBars.Instance.SpoolIsEnabled = SpoolUpBeforeFiring;
			WeaponStatsBars.Instance.SpoolFillAmmount = 0;
		}

		void OnDisable()
		{
			print("Disabled");
			StopFiring();
			StopAllCoroutines();

			WeaponStatsBars.Instance.OverheatIsEnabled = false;

			WeaponStatsBars.Instance.SpoolIsEnabled = false;

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

		public override void FireKeyHeld(bool state)
		{

			if (!state || (_overheated && Overheat))
			{
				StopFiring();
				return;
			}

			if (SingleFire && _firedShot)
			{
				_spoolTime = 0f;


				if (weaponProjectile != null) weaponProjectile.EndFireProjectile();
				_beginSpooling = false;
				if (weaponAmmo != null) weaponAmmo.StopReducingAmmo();
				return;
			}

			_beginSpooling = true;



			if (weaponAmmo != null && !weaponAmmo.HasAmmo())
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
				_spoolTime = 0f;
				StopFiring();
				return;
			}

			_firing = true;

			// do cool things

			if (_timeUntilNextAttack <= 0)
			{
				_timeUntilNextAttack = 1f;

				if (weaponAmmo != null) weaponAmmo.StartReducingAmmo();

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

			weaponAudio = GetComponent<WeaponAudioBase>();

			if (weaponAudio == null)
			{
				Debug.Log("You can add any scripts that inherit " + nameof(WeaponAudioBase) + " to this to play sounds!");
			}

			weaponProjectile = GetComponent<WeaponProjectileBase>();

			if (weaponProjectile == null)
			{
				throw new NullReferenceException("Cannot use weapon with not projectile script! Please add one that has the " + nameof(WeaponProjectileBase) + " interface attached.");
			}

			weaponAmmo = GetComponent<WeaponAmmoBase>();

			if (weaponAmmo == null)
			{
				Debug.LogWarning("Are you sure you want to use weapon with no " + nameof(WeaponAmmoBase) + "! Do you want ammo? cos you might need it.");
			}

			if (weaponAmmo != null) weaponAmmo.ResetAllAmmo();
		}

		public override void AimKeyHeld(bool state)
		{

		}

		public override void ReloadKeyPressed()
		{
			if (weaponAmmo != null) weaponAmmo.Reload();

			if (weaponAudio != null)
			{
				weaponAudio.Reload();
			}
		}

		public override void SpecialKeyPressed()
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
