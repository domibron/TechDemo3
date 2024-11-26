using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.InputHandling
{

	public class PlayerInputHandler : MonoBehaviour
	{

		public KeyCode FireKey = KeyCode.Mouse0;
		public KeyCode AimKey = KeyCode.Mouse1;
		public KeyCode ReloadKey = KeyCode.R;
		public KeyCode SpecialWeaponKey = KeyCode.B;

		public KeyCode ThrowableHotKey = KeyCode.G;

		public KeyCode AlphaKey1 = KeyCode.Alpha1;
		public KeyCode AlphaKey2 = KeyCode.Alpha2;
		public KeyCode AlphaKey3 = KeyCode.Alpha3;
		public KeyCode AlphaKey4 = KeyCode.Alpha4;
		public KeyCode AlphaKey5 = KeyCode.Alpha5;

		public KeyCode Ablility1Key = KeyCode.Q;
		public KeyCode Ablility2Key = KeyCode.E;
		public KeyCode Ablility3Key = KeyCode.T;

		public Vector3 GetMovementAsVector3()
		{
			return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		}

		public Vector2 GetMouseAsVector2()
		{
			return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
		}

		public bool GetKey(KeyCode key)
		{
			return Input.GetKey(key);
		}

		public bool GetKeyDown(KeyCode key)
		{
			return Input.GetKeyDown(key);
		}

		public bool GetKeyUp(KeyCode key)
		{
			return Input.GetKeyUp(key);
		}

	}

}
