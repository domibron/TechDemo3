using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
	public class WeaponManager : MonoBehaviour
	{

		//Publics
		public IWeapon CurrentSelectedWeapon;

		public List<GameObject> WeaponsInInventory = new List<GameObject>();


		//Privates
		private PlayerInputHandler _playerInputHandler;

		void Start()
		{
			SetUpWeaponManager();
		}

		void Update()
		{

		}

		public void SetUpWeaponManager()
		{
			_playerInputHandler = GetComponent<PlayerInputHandler>();
		}
	}
}
