using System.Collections;
using System.Collections.Generic;
using Project.InputHandling;
using UnityEngine;

namespace Project.PlayerSystems
{
	[RequireComponent(typeof(PlayerInputHandler))]
	public class MouseLookController : MonoBehaviour
	{

		// publics
		public Transform CameraHolderTransform;
		public Transform PlayerTransformObject;

		public float Sensitivity = 1f;

		public float MinXAxisRotation = -80f;

		public float MaxXAxisRotation = 80f;


		// privates
		private PlayerInputHandler _playerInputHandler;

		private float XRotationAxis;

		void Start()
		{
			SetUpMouseController();
		}

		void Update()
		{
			HandleLook();
		}

		private void SetUpMouseController()
		{

			if (PlayerTransformObject == null) PlayerTransformObject = transform;

			_playerInputHandler = GetComponent<PlayerInputHandler>();
		}

		private void HandleLook()
		{
			Vector2 mouseVector = _playerInputHandler.GetMouseAsVector2();

			XRotationAxis -= mouseVector.y * Sensitivity;

			XRotationAxis = Mathf.Clamp(XRotationAxis, MinXAxisRotation, MaxXAxisRotation);

			CameraHolderTransform.localRotation = Quaternion.Euler(XRotationAxis, 0, 0);

			PlayerTransformObject.Rotate(0, mouseVector.x * Sensitivity, 0);

		}
	}
}
