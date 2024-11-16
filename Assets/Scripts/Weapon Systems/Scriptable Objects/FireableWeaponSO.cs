using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
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

	[CreateAssetMenu(menuName = "Weapons/Fireable/FireableWeaponSO")]
	public class FireableWeaponSO : WeaponSOBase
	{

		/// <summary>
		/// The max size of the ammo pool for this weapon.
		/// </summary>
		[Header("Fireable Weapon Settings"), Tooltip("The max size of the ammo pool for this weapon.")]
		public int MaxAmmo = 220;

		/// <summary>
		/// The ammount of ammo to scoop out of the ammo pool to fill up the weapon.
		/// </summary>
		[Tooltip("The size of the magazine to scoop ammo with.")]
		public int MagazineSize = 30;

		/// <summary>
		/// How long it will take for the weapon to reload.
		/// </summary>
		[Tooltip("How long it will take for the weapon to reload.")]
		public float ReloadSpeed = 1f;

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
	}
}
