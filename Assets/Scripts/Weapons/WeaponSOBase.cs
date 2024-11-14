using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{

	[CreateAssetMenu(menuName = "TechDemo3/WeaponSOBase")]
	public class WeaponSOBase : ScriptableObject
	{
		public string WeaponName = "Weapon Name";

		public float Damage = 10f;

		public float FireRate = 5f;

		public int MaxAmmo = 220;

		public int MaxAmmoInMagazine = 30;
	}
}
