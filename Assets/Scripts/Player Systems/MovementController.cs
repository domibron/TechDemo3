using System.Collections;
using System.Collections.Generic;
using Project.InputHandling;
using UnityEngine;

namespace Project.PlayerSystems
{


	[RequireComponent(typeof(PlayerInputHandler), typeof(CharacterController))]
	public class MovementController : MonoBehaviour
	{

		//Publics
		[Header("Sprinting")]
		public float SprintSpeed = 10;

		public float SprintAccelRate = 1;

		public float MaxStrafeAngle = 0.5f;

		[Header("Walking")]

		public float WalkSpeed = 7;

		public float WalkAccelRate = 1;

		public float DeccelRate = 1;

		[Header("Air Strafe")]

		public float AirStrafeSpeed = 4;

		public float AirStrafeRate = 1;

		[Header("Stamina")]

		public float StamUsageRate = 5;

		public float StamRechargeSpeed = 3f;

		[Header("Gravity")]
		public Vector3 Gravity = new Vector3(0, -9.81f, 0);

		public float IdleGravity = -5;

		[Header("Jump Height")]

		public float JumpHeight = 1f;


		[Header("Crouching")]
		public float CrouchHeight = 1;

		public Transform CameraHolder;

		// Publics but hidden from inspector. lmao, its not.
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

		private bool _movementModified = false;
		private bool _gravEnabled = false;

		private bool _useStam = false;
		private bool _drainedStam = false;

		private float _stamBar = 1f;


		void Start()
		{
			SetUpMovementController();
		}

		void Update()
		{
			if (_stamBar > 0 && _useStam)
			{
				_stamBar -= Time.deltaTime * (1 / StamUsageRate);
			}
			else if (_stamBar < 1 && !_useStam)
			{
				_stamBar += Time.deltaTime * (1 / StamRechargeSpeed);
			}

			if (_stamBar <= 0) _drainedStam = true;
			else if (_stamBar >= 0.9f) _drainedStam = false;

			HandleGroundCheck();

			HandleJumping();

			HandleGravity();

			HandleCrouching();

			DealWithCameraPosition();

			AddMovementToVelocity();

			MovePlayerUsingVelocity(); // ! Must be last. (unless otherwise)

			_movementModified = false;
			_gravEnabled = true;
		}

		public bool GetGrounded()
		{
			return _isGrounded;
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

		#region AddToVel
		public void AddToVelocity(Vector3 velocityToAdd, bool disableGravity = false)
		{
			Velocity += velocityToAdd;
			_movementModified = true;
			_gravEnabled = !disableGravity;
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

			if (inputVector.magnitude != 0 && _isGrounded && !_movementModified)
			{
				if (_playerInputHandler.GetKey(KeyCode.LeftShift) && Vector3.Dot(inputVector, transform.forward) > MaxStrafeAngle && !_isCrouched && _stamBar > 0 && !_drainedStam)
				{
					_useStam = true;
					Velocity += ((inputVector.normalized * SprintSpeed) - VelocityWithNoY) * SprintAccelRate;


				}
				else
				{
					_useStam = false;
					// float mult = Vector3.Dot(Velocity, inputVector.normalized);

					Velocity += ((inputVector.normalized * WalkSpeed) - VelocityWithNoY) * WalkAccelRate;
				}


			}
			else if (inputVector.magnitude != 0 && (!_isGrounded || _movementModified))
			{

				_useStam = false;


				Vector3 calcVec = (inputVector.normalized * AirStrafeSpeed) * AirStrafeRate;

				if (calcVec.magnitude < VelocityWithNoY.magnitude) return;

				Velocity += calcVec;


			}
			else if (_isGrounded && !_movementModified)
			{

				_useStam = false;


				Velocity += -VelocityWithNoY * DeccelRate;

				//if (Velocity.magnitude < 0.1f) Velocity = Vector3.zero;
			}
			else
			{
				_useStam = false;
			}

			// if (!_movementModified) ApplyFriction();
		}
		#endregion

		// private void Accelerate(Vector3 wishdir, float wishspeed, float accel)
		// {
		// 	float addspeed;
		// 	float accelspeed;
		// 	float currentspeed;

		// 	currentspeed = Vector3.Dot(Velocity, wishdir);
		// 	print(currentspeed);
		// 	addspeed = wishspeed - currentspeed;
		// 	if (addspeed <= 0)
		// 	{
		// 		return;
		// 	}
		// 	accelspeed = accel * Time.deltaTime * wishspeed;

		// 	if (accelspeed > addspeed)
		// 	{
		// 		accelspeed = addspeed;
		// 	}

		// 	Velocity += accelspeed * wishdir;
		// }

		// yes, it was.
		// private void ApplyFriction()
		// {
		// 	float speed;
		// 	float newspeed;
		// 	float control;
		// 	float friction;
		// 	float drop;

		// 	speed = Velocity.magnitude;

		// 	if (speed < 1)
		// 	{
		// 		Velocity.x = 0;
		// 		Velocity.z = 0;
		// 		return;
		// 	}

		// 	drop = 0;

		// 	//apply ground friction
		// 	if (_isGrounded)
		// 	{
		// 		friction = Friction;
		// 		control = speed < StopSpeed ? StopSpeed : speed;
		// 		drop += control * friction * Time.deltaTime;
		// 	}



		// 	//scale the velocity
		// 	newspeed = speed - drop;
		// 	if (newspeed < 0)
		// 	{
		// 		newspeed = 0;
		// 	}
		// 	newspeed /= speed;
		// 	Velocity *= newspeed;
		// }

		#region HandleGroundCheck
		private void HandleGroundCheck()
		{
			Vector3 tartgetPos = transform.position + new Vector3(0, -(_characterController.height / 2), 0);
			float targetRad = _characterController.radius;

			_isGrounded = Physics.CheckSphere(tartgetPos, targetRad, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS);
		}
		#endregion

		#region HandleGravity
		private void HandleGravity()
		{
			if (!_gravEnabled) return;

			if (_isGrounded)
			{
				if (Velocity.y < IdleGravity)
				{
					Velocity.y += IdleGravity - Velocity.y;
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
			if (_playerInputHandler.GetKeyDown(KeyCode.Space) && _isGrounded)
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

				if (Physics.SphereCast(transform.position, _characterController.radius, transform.up, out RaycastHit hit, _standingHeight, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
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

		void OnCollisionEnter(Collision other)
		{
			if (_movementModified)
			{
				Velocity = _characterController.velocity;
			}
		}

		void OnCollisionStay(Collision other)
		{
			if (_movementModified)
			{
				Velocity = _characterController.velocity;
			}
		}
	}
}
