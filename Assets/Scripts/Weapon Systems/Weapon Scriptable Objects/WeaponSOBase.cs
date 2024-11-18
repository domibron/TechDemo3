using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{

	[CreateAssetMenu(menuName = "Weapons/WeaponSOBase")]
	public class WeaponSOBase : ScriptableObject
	{

		/// <summary>
		/// The name of the weapon and display name.
		/// </summary>
		[Header("Base Weapon Settings"), Tooltip("The name of the weapon and display name.")]
		public string WeaponName = "Weapon Name";

		/// <summary>
		/// How much damage each time the weapon hits.
		/// </summary>
		[Tooltip("The name of the weapon and display name.")]
		public float Damage = 10f;

		/// <summary>
		/// How Quick the weapon attacks.
		/// </summary>
		[Tooltip("How Quick the weapon attacks.")]
		public float FireRate = 5f;

		/// <summary>
		/// The max range of this melee weapon.
		/// </summary>
		[Tooltip("The max range of this melee weapon.")]
		public float Range = 999f;
	}
}
