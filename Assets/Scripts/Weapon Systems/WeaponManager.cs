using System.Collections;
using System.Collections.Generic;
using Project.InputHandling;
using UnityEngine;
using TMPro;

namespace Project.WeaponSystems
{
	public class WeaponManager : MonoBehaviour
	{

		//Publics
		public BaseWeapon CurrentSelectedWeapon;

		public List<GameObject> WeaponsInInventory = new List<GameObject>();


		public Transform TargetWeaponParent;


		// TODO move into another manager, this is UI not weapon inventory manager.
		public TMP_Text WeaponNameDisplay;
		public TMP_Text WeaponAmmoDisplay;


		//Privates
		private PlayerInputHandler _playerInputHandler;

		private int _currentlySelectedSlot = 0;

		void Start()
		{
			SetUpWeaponManager();

			Cursor.lockState = CursorLockMode.Locked;
		}

		void Update()
		{
			if (CurrentSelectedWeapon != null)
			{
				CurrentSelectedWeapon.FireKeyHeld(_playerInputHandler.GetKey(_playerInputHandler.FireKey));
			}

			if (CurrentSelectedWeapon != null)
			{
				CurrentSelectedWeapon.AimKeyHeld(_playerInputHandler.GetKey(_playerInputHandler.AimKey));
			}

			if (_playerInputHandler.GetKeyDown(_playerInputHandler.ReloadKey) && CurrentSelectedWeapon != null)
			{
				CurrentSelectedWeapon.ReloadKeyPressed();
			}

			if (_playerInputHandler.GetKeyDown(_playerInputHandler.SpecialWeaponKey) && CurrentSelectedWeapon != null)
			{
				CurrentSelectedWeapon.SpecialKeyPressed();
			}

			// if (CurrentSelectedWeapon != null)
			// {
			// 	CurrentSelectedWeapon.FireKeyUpdate(_playerInputHandler.GetKey(_playerInputHandler.FireKey));
			// }

			// TODO move into another manager, this is UI not weapon inventory manager.
			UpdateDisplays(); // Updates the display


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

		// TODO move into another manager, this is UI not weapon inventory manager.
		private void UpdateDisplays()
		{
			if (CurrentSelectedWeapon == null)
			{
				WeaponNameDisplay.text = "Nothing";
				WeaponAmmoDisplay.text = "";
				return;
			}

			WeaponNameDisplay.text = CurrentSelectedWeapon.DisplayName;
			WeaponAmmoDisplay.text = CurrentSelectedWeapon.AmmoDisplay;
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

		private BaseWeapon GetWeaponComponent(int slot)
		{
			return WeaponsInInventory[slot].GetComponent<BaseWeapon>();
		}
	}
}
