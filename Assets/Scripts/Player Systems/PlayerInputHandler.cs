using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{

	public class PlayerInputHandler : MonoBehaviour
	{

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
