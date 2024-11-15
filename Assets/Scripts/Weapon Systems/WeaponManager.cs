using System.Collections;
using System.Collections.Generic;
using Project.InputHandling;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class WeaponManager : MonoBehaviour
	{

		//Publics
		public IWeapon CurrentSelectedWeapon;

		public List<GameObject> WeaponsInInventory = new List<GameObject>();


		public Transform TargetWeaponParent;

		//Privates
		private PlayerInputHandler _playerInputHandler;

		private int _currentlySelectedSlot = 0;

		void Start()
		{
			SetUpWeaponManager();
		}

		void Update()
		{
			if (_playerInputHandler.GetKey(KeyCode.Mouse0) && CurrentSelectedWeapon != null)
			{
				CurrentSelectedWeapon.Fire();
			}

			if (_playerInputHandler.GetKey(KeyCode.Mouse1) && CurrentSelectedWeapon != null)
			{
				CurrentSelectedWeapon.Aim();
			}

			if (_playerInputHandler.GetKey(KeyCode.R) && CurrentSelectedWeapon != null)
			{
				CurrentSelectedWeapon.RPressed();
			}



			// weapon switching.

			// Fists
			if (_playerInputHandler.GetKey(KeyCode.Alpha3))
			{
				SwitchWeapon(0);
			}



			if (WeaponsInInventory.Count <= 0) return;



			// weapons
			if (_playerInputHandler.GetKey(KeyCode.Alpha1))
			{
				SwitchWeapon(1);
			}

			if (_playerInputHandler.GetKey(KeyCode.Alpha2))
			{
				SwitchWeapon(2);
			}
		}


		private void SetUpWeaponManager()
		{
			_playerInputHandler = GetComponent<PlayerInputHandler>();

			if (WeaponsInInventory.Count > 0)
			{
				SwitchWeapon(0);
			}
		}

		/// <summary>
		/// Selects the weapon in slot if there is one. 
		/// </summary>
		/// <param name="slot">The slot to attempt to equip</param>
		private void SwitchWeapon(int slot)
		{
			if (WeaponsInInventory.Count < 1 || WeaponsInInventory.Count - 1 < slot) return;

			for (int i = 0; i < WeaponsInInventory.Count; i++)
			{
				if (i == slot)
				{
					WeaponsInInventory[i].SetActive(true);
					CurrentSelectedWeapon = GetWeaponComponent(i);
				}
				else
				{
					WeaponsInInventory[i].SetActive(false);
				}
			}
		}

		private void SwitchWeapon()
		{
			if (WeaponsInInventory.Count < 1) return;

			_currentlySelectedSlot++;


			if (_currentlySelectedSlot >= WeaponsInInventory.Count)
			{
				_currentlySelectedSlot = 0;
			}

			for (int i = 0; i < WeaponsInInventory.Count; i++)
			{
				if (i == _currentlySelectedSlot)
				{
					WeaponsInInventory[i].SetActive(true);
					CurrentSelectedWeapon = GetWeaponComponent(i);
				}
				else
				{
					WeaponsInInventory[i].SetActive(false);
				}
			}
		}

		public void EquipWeapon(GameObject prefab, int targetSlot)
		{
			GameObject collectedWeapon = Instantiate(prefab, TargetWeaponParent);
			collectedWeapon.transform.localPosition = Vector3.zero;
			collectedWeapon.transform.localRotation = Quaternion.identity;



			if (WeaponsInInventory[targetSlot] != null)
			{
				Destroy(WeaponsInInventory[targetSlot]);
			}


			WeaponsInInventory[targetSlot] = collectedWeapon;

			CurrentSelectedWeapon = GetWeaponComponent(targetSlot);

		}

		private IWeapon GetWeaponComponent(int slot)
		{
			return WeaponsInInventory[slot].GetComponent<IWeapon>();
		}
	}
}
