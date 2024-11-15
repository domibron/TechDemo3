using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{

	[CreateAssetMenu(menuName = "Weapons/WeaponSOBase")]
	public class WeaponSOBase : ScriptableObject
	{
		public string WeaponName = "Weapon Name";

		public float Damage = 10f;

		public float FireRate = 5f;
	}
}
