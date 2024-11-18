using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class WeaponAmmoMagazine : MonoBehaviour, IWeaponAmmo
	{

		string IWeaponAmmo.AmmoString => GetAmmoAsString();

		public float MaxAmmoPool = 220;

		public float MagazineSize = 30;

		public float AmmoReductionWhenFired = 1;

		public float ReloadSpeed = 1f;

		protected bool _isReloading = false;


		private float _currentAmmoInWeapon;

		private float _currentAmmoPool;


		bool IWeaponAmmo.HasAmmo()
		{
			return GetIfWeaponHasAmmo();
		}


		void IWeaponAmmo.StartReducingAmmo()
		{
			ReduceAmmoInMagazine();
		}

		void IWeaponAmmo.StopReducingAmmo()
		{

		}

		void IWeaponAmmo.Reload()
		{
			ReloadWeapon();
		}

		void IWeaponAmmo.ResetAllAmmo()
		{
			ResetWeaponAmmo();
		}

		private bool GetIfWeaponHasAmmo()
		{
			return _currentAmmoInWeapon > 0;
		}

		protected virtual void ResetWeaponAmmo()
		{
			_currentAmmoInWeapon = MagazineSize;

			_currentAmmoPool = MaxAmmoPool;
		}

		private void ReduceAmmoInMagazine()
		{
			if (GetIfWeaponHasAmmo())
				_currentAmmoInWeapon -= AmmoReductionWhenFired;
		}

		protected virtual void ReloadWeapon()
		{
			if (_isReloading || _currentAmmoInWeapon >= MagazineSize + 1) return;

			StartCoroutine(ReloadOverTime());
		}

		protected virtual IEnumerator ReloadOverTime()
		{
			_isReloading = true;

			yield return new WaitForSeconds(ReloadSpeed);

			if (_currentAmmoPool >= MagazineSize)
			{
				// We add one because we can chamber a round. we dont want to lose extra ammo either, because this is not a realistic game.
				float ammountToTake = (MagazineSize + (_currentAmmoInWeapon > 0 ? 1 : 0)) - _currentAmmoInWeapon;

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
			else return $"Ammo:\n{_currentAmmoInWeapon:F0} / {_currentAmmoPool:F0}]";
		}


	}
}
