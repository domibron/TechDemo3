using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	[CreateAssetMenu(menuName = "Weapons/Melee/MeleeWeaponSO")]
	public class MeleeWeaponSO : WeaponSOBase
	{
		/// <summary>
		/// The max range of this melee weapon.
		/// </summary>
		[Header("Melee Weapon Settings"), Tooltip("The max range of this melee weapon.")]
		public float Range = 1f;
	}
}
