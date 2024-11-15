using System.Collections;
using System.Collections.Generic;
using Project.InputHandling;
using UnityEngine;

namespace Project.PlayerSystems
{


	[RequireComponent(typeof(PlayerInputHandler), typeof(CharacterController))]
	public class CCMovementController : MonoBehaviour
	{

		//Publics
		[Header("Legs")]
		public float SprintSpeed = 10;

		public float SprintAccelRate = 1;

		public float MaxStrafeAngle = 0.5f;

		public float WalkSpeed = 7;

		public float WalkAccelRate = 1;

		public float DeccelRate = 1;

		[Header("Jumping")]
		public Vector3 Gravity = new Vector3(0, -9.81f, 0);

		public float JumpHeight = 1f;

		[Header("Crouching")]
		public float CrouchHeight = 1;

		public Transform CameraHolder;


		// Publics but hidden from inspector.
		[Header("No Touch!")]
		public Vector3 Velocity;

		// Privates
		private CharacterController _characterController;

		private PlayerInputHandler _playerInputHandler;

		private bool _isGrounded = false;

		private bool _isCrouched = false;

		private bool _stuckCrouched = false;

		private float _standingHeight;

		private float _camHeightStanding;
		private float _camHeightCrouched;


		void Start()
		{
			SetUpMovementController();
		}

		void Update()
		{
			HandleGroundCheck();

			HandleJumping();

			HandleGravity();

			HandleCrouching();

			DealWithCameraPosition();

			AddMovementToVelocity();

			MovePlayerUsingVelocity(); // ! Must be last. (unless otherwise)
		}

		#region SetUpMovementController
		private void SetUpMovementController()
		{
			_characterController = GetComponent<CharacterController>();

			_playerInputHandler = GetComponent<PlayerInputHandler>();

			_standingHeight = _characterController.height;

			_camHeightStanding = CameraHolder.localPosition.y;

			_camHeightCrouched = (CrouchHeight / 2f) * ((_camHeightStanding * 2f) / _standingHeight);
		}
		#endregion


		#region AddMovementToVelocity
		private void AddMovementToVelocity()
		{
			Vector3 inputVector = _playerInputHandler.GetMovementAsVector3();

			inputVector.y = 0;

			inputVector = transform.forward * inputVector.z + transform.right * inputVector.x;

			Vector3 VelocityWithNoY = Velocity;
			VelocityWithNoY.y = 0;

			if (inputVector.magnitude != 0 && _isGrounded)
			{
				if (_playerInputHandler.GetKey(KeyCode.LeftShift) && Vector3.Dot(inputVector, transform.forward) > MaxStrafeAngle && !_isCrouched)
				{

					Velocity += ((inputVector.normalized * SprintSpeed) - VelocityWithNoY) * SprintAccelRate;
				}
				else
				{
					Velocity += ((inputVector.normalized * WalkSpeed) - VelocityWithNoY) * WalkAccelRate;

				}


			}
			else if (_isGrounded)
			{
				Velocity += -VelocityWithNoY * DeccelRate;
			}
		}
		#endregion

		#region HandleGroundCheck
		private void HandleGroundCheck()
		{
			Vector3 tartgetPos = transform.position + new Vector3(0, -(_characterController.height / 2), 0);
			float targetRad = _characterController.radius;

			_isGrounded = Physics.CheckSphere(tartgetPos, targetRad, StaticData.LAYER_WITH_ONLY_PLAYER_IGNORED);
		}
		#endregion

		#region HandleGravity
		private void HandleGravity()
		{
			if (_isGrounded)
			{
				if (Velocity.y < Gravity.y)
				{
					Velocity.y += Gravity.y - Velocity.y;
				}
				else
				{
					Velocity += Gravity * Time.deltaTime;
				}
			}
			else
			{
				Velocity += Gravity * Time.deltaTime;
			}
		}
		#endregion

		#region HandleJumping
		private void HandleJumping()
		{
			if (_playerInputHandler.GetKeyDown(KeyCode.Space))
			{
				float JumpForce = Mathf.Sqrt(2f * JumpHeight * Mathf.Abs(Gravity.y));

				if (Velocity.y < 0)
				{
					JumpForce = JumpForce + -Velocity.y;
				}

				Velocity.y += JumpForce;
			}
		}
		#endregion

		#region HandleCrouching
		private void HandleCrouching()
		{
			if (_playerInputHandler.GetKey(KeyCode.C) || _stuckCrouched)
			{
				_isCrouched = true;
			}
			else
			{
				_isCrouched = false;
			}

			if (_isCrouched)
			{
				_characterController.height = CrouchHeight;

				Vector3 topPoint = transform.position + new Vector3(0, (_standingHeight / 2f - CrouchHeight / 2f) + _characterController.radius, 0);
				Vector3 bottomPoint = transform.position + new Vector3(0, (_standingHeight / 2f - CrouchHeight / 2f) - _characterController.radius, 0);

				Debug.DrawLine(topPoint, transform.position, Color.red);
				Debug.DrawLine(bottomPoint, transform.position, Color.blue);

				if (Physics.SphereCast(transform.position, _characterController.radius, transform.up, out RaycastHit hit, _standingHeight, StaticData.LAYER_WITH_ONLY_PLAYER_IGNORED))
				{
					_stuckCrouched = true;
				}
				else
				{
					_stuckCrouched = false;
				}
			}
			else
			{
				_characterController.height = _standingHeight;

			}
		}
		#endregion

		#region DealWithCameraPosition
		private void DealWithCameraPosition()
		{
			if (_isCrouched)
			{
				CameraHolder.localPosition = new Vector3(0, _camHeightCrouched, 0);
			}
			else
			{
				CameraHolder.localPosition = new Vector3(0, _camHeightStanding, 0);

			}
		}
		#endregion

		#region MovePlayerUsingVelocity
		private void MovePlayerUsingVelocity()
		{
			_characterController.Move(Velocity * Time.deltaTime);
		}
		#endregion
	}
}
