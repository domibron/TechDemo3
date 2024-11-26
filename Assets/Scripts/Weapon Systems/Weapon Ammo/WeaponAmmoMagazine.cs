using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class WeaponAmmoMagazine : WeaponAmmoBase
	{

		public override string AmmoString => GetAmmoAsString();

		public float MaxAmmoPool = 220;

		public float MagazineSize = 30;

		public float AmmoReductionWhenFired = 1;

		public bool AllowExtraRoundInChamber = true;

		public float ReloadSpeed = 1f;

		protected bool _isReloading = false;


		private float _currentAmmoInWeapon;

		private float _currentAmmoPool;


		void OnDisable()
		{
			StopAllCoroutines();
			_isReloading = false;
		}

		public override bool HasAmmo()
		{
			return _currentAmmoInWeapon > 0;
		}

		public override void Reload()
		{
			if (_isReloading || _currentAmmoInWeapon >= MagazineSize + (AllowExtraRoundInChamber ? 1 : 0)) return;

			StartCoroutine(ReloadOverTime());
		}

		public override void ResetAllAmmo()
		{
			_currentAmmoInWeapon = MagazineSize;

			_currentAmmoPool = MaxAmmoPool;
		}

		public override void StartReducingAmmo()
		{
			if (HasAmmo())
				_currentAmmoInWeapon -= AmmoReductionWhenFired;
		}

		public override void StopReducingAmmo()
		{

		}

		protected virtual IEnumerator ReloadOverTime()
		{
			_isReloading = true;

			yield return new WaitForSeconds(ReloadSpeed);

			if (_currentAmmoPool >= MagazineSize)
			{
				// We add one because we can chamber a round. we dont want to lose extra ammo either, because this is not a realistic game.
				float ammountToTake = (MagazineSize + (_currentAmmoInWeapon > 0 ? (AllowExtraRoundInChamber ? 1 : 0) : 0)) - _currentAmmoInWeapon;

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
		}

		protected virtual string GetAmmoAsString()
		{
			if (_isReloading) return $"Ammo:\n[Reloading]";
			else return $"Ammo:\n{_currentAmmoInWeapon:F0} / {_currentAmmoPool:F0}";
		}


	}
}
