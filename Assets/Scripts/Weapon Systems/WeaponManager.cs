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

		public GameObject[] WeaponsInInventory;


		public GameObject Key4WeaponObject;


		public GameObject Key5WeaponObject;



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
			if (_playerInputHandler.GetKey(_playerInputHandler.AlphaKey3))
			{

				SwitchWeapon(0);
			}



			if (WeaponsInInventory.Length <= 0) return;


			// weapons
			if (_playerInputHandler.GetKey(_playerInputHandler.AlphaKey1))
			{

				SwitchWeapon(1);
			}

			if (_playerInputHandler.GetKey(_playerInputHandler.AlphaKey2))
			{

				SwitchWeapon(2);
			}

			if (_playerInputHandler.GetKey(_playerInputHandler.AlphaKey4))
			{
				SwitchWeapon(-1);

				if (Key5WeaponObject != null) Key5WeaponObject.SetActive(false);

				if (Key4WeaponObject != null)
				{
					CurrentSelectedWeapon = Key4WeaponObject.GetComponent<BaseWeapon>();
					Key4WeaponObject.SetActive(true);
				}
			}

			if (_playerInputHandler.GetKey(_playerInputHandler.AlphaKey5))
			{
				SwitchWeapon(-1);
				if (Key4WeaponObject != null) Key4WeaponObject.SetActive(false);

				if (Key5WeaponObject != null)
				{
					CurrentSelectedWeapon = Key5WeaponObject.GetComponent<BaseWeapon>();
					Key5WeaponObject.SetActive(true);
				}
			}
		}


		private void SetUpWeaponManager()
		{
			_playerInputHandler = GetComponent<PlayerInputHandler>();

			if (WeaponsInInventory.Length > 0)
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
			if (WeaponsInInventory.Length < 1 || WeaponsInInventory.Length - 1 < slot) return;

			if (WeaponsInInventory[slot] == null) return;

			if (slot >= 0)
			{
				if (Key4WeaponObject != null) Key4WeaponObject.SetActive(false);
				if (Key5WeaponObject != null) Key5WeaponObject.SetActive(false);
			}

			for (int i = 0; i < WeaponsInInventory.Length; i++)
			{
				if (i == slot)
				{
					if (WeaponsInInventory[i] != null)
					{
						WeaponsInInventory[i].SetActive(true);
						CurrentSelectedWeapon = GetWeaponComponent(i);
					}
				}
				else
				{
					if (WeaponsInInventory[i] != null) WeaponsInInventory[i].SetActive(false);
				}
			}
		}

		private void SwitchWeapon()
		{
			if (WeaponsInInventory.Length < 1) return;

			_currentlySelectedSlot++;


			if (_currentlySelectedSlot >= WeaponsInInventory.Length)
			{
				_currentlySelectedSlot = 0;
			}

			for (int i = 0; i < WeaponsInInventory.Length; i++)
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
