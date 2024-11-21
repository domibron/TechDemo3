using Project.PlayerSystems;
using UnityEngine;
using Project.WeaponSystems;
using Project.StatusEffects;

namespace Project.WeaponSystems.SpecialWeaponFuntionality
{
	[RequireComponent(typeof(LineRenderer))]
	public class GrappleHookWeapon : BaseWeapon
	{
		public override string DisplayName => WeaponSO.WeaponName;

		public override string AmmoDisplay => "";

		public float StunDuration = 1f;

		public float PullForce = 10f;
		public float UnderForce = 20f;

		public float PullAccelerationRate = 0.5f;

		public LineRenderer GrappleLineRenderer;

		public Transform GrappleStart;

		public float GravMult = 0.07f;

		[SerializeField]
		private ReturnableWeaponProjectileBase _weaponProjectile;

		private CCMovementController _ccMovementController;

		private Transform _grapplePointTransform;
		private Vector3 _grapplePointVec;

		private bool _isGrappling = false;

		private bool _isPulling = false;

		private Rigidbody _otherRigid;
		private CharacterController _otherCharacterController;

		private bool _fired = false;


		void Start()
		{
			SetUpWeapon();
		}


		void Update()
		{
			if (_isPulling)
			{
				_grapplePointTransform.GetComponent<IStunable>()?.Stun(StunDuration);

				if (_otherRigid != null)
				{
					_otherRigid.AddForce((transform.position - _grapplePointTransform.position).normalized * PullForce, ForceMode.Impulse);
				}
				else if (_otherCharacterController != null)
				{
					_otherCharacterController.Move((transform.position - _grapplePointTransform.position).normalized * PullForce * Time.deltaTime);
				}
			}
			else if (_isGrappling)
			{

				Vector3 counterVel = Vector3.zero;
				Vector3 velNoY = new Vector3(_ccMovementController.Velocity.x, 0, _ccMovementController.Velocity.z);

				Vector3 targNoY = _grapplePointVec - transform.position;
				targNoY.y = 0f;


				// if (Vector3.Dot(velNoY, targNoY) < 0.2f && Vector3.Distance(_grapplePointVec, transform.position) > 2f)
				// {
				// counterVel = (_grapplePointVec - transform.position).normalized * 3f;
				// counterVel += -velNoY.normalized * 3f;
				// }


				counterVel += -velNoY * 0.15f;


				float power = PullForce;


				Vector3 grav = _ccMovementController.Gravity * GravMult;

				if ((_grapplePointVec - transform.position).normalized.y > 0 && Vector3.Dot((transform.position - _grapplePointVec).normalized, Vector3.down) > 0.5f)
				{
					grav = -_ccMovementController.Gravity * GravMult;

					power = UnderForce;

					if (_ccMovementController.Velocity.y < 0 && _ccMovementController.GetGrounded())
					{
						_ccMovementController.Velocity.y = 0f;
					}
				}
				else if ((_grapplePointVec - transform.position).normalized.y < 0)
				{
					grav = _ccMovementController.Gravity * (Vector3.Dot((transform.position - _grapplePointVec).normalized, Vector3.up) / 2f);


				}

				if (_ccMovementController.GetGrounded())
				{
					grav += _ccMovementController.Gravity * GravMult;
					if (_ccMovementController.Velocity.y < _ccMovementController.Gravity.y)
					{
						_ccMovementController.Velocity.y = _ccMovementController.Gravity.y;
					}
				}

				Vector3 ropePullForce = (_grapplePointVec - transform.position).normalized * power * PullAccelerationRate;

				if (Vector3.Distance(_grapplePointVec, transform.position) < 2f)
				{
					ropePullForce = Vector3.zero;
					if (grav.y > -1f)
					{
						grav = _ccMovementController.Gravity * GravMult;
					}
				}

				if (_ccMovementController.Velocity.y < _ccMovementController.Gravity.y)
				{
					grav += new Vector3(0, _ccMovementController.Velocity.y - _ccMovementController.Gravity.y);
				}

				_ccMovementController.AddToVelocity((ropePullForce + counterVel + grav) * Time.deltaTime, true);
			}

			if (_isGrappling && _grapplePointTransform != null)
			{
				GrappleLineRenderer.enabled = true;
				GrappleLineRenderer.SetPosition(0, GrappleStart.position);
				GrappleLineRenderer.SetPosition(1, _grapplePointVec);
			}
			else if (_isPulling && _grapplePointTransform != null)
			{
				GrappleLineRenderer.enabled = true;
				GrappleLineRenderer.SetPosition(0, GrappleStart.position);
				GrappleLineRenderer.SetPosition(1, _grapplePointTransform.position);
			}
			else
			{
				GrappleLineRenderer.enabled = false;

			}
		}


		void OnDisable()
		{
			StopFire();
		}

		private void StopFire()
		{
			_isGrappling = false;
			_isPulling = false;
			_otherRigid = null;
			_otherCharacterController = null;

			_weaponProjectile.EndFireProjectile();

			GrappleLineRenderer.enabled = false;

			_fired = false;
		}


		public override void AimKeyHeld(bool state)
		{

		}

		public override void FireKeyHeld(bool state)
		{
			if (!state)
			{

				StopFire();

				return;
			}

			if (_fired) return;
			_fired = true;

			(_grapplePointTransform, _grapplePointVec) = _weaponProjectile.StartFireProjectile(WeaponSO.Damage, WeaponSO.Range);

			if (_grapplePointTransform == null) return;

			if (_grapplePointTransform.GetComponent<Rigidbody>() != null || _grapplePointTransform.GetComponent<CharacterController>())
			{
				_isPulling = true;

				_otherRigid = _grapplePointTransform.GetComponent<Rigidbody>();
				_otherCharacterController = _grapplePointTransform.GetComponent<CharacterController>();

				return;
			}

			_isGrappling = true;
		}

		public override void ReloadKeyPressed()
		{
			// no ammo.
		}

		public override void SpecialKeyPressed()
		{

		}

		protected override void SetUpWeapon()
		{
			_weaponProjectile = GetComponent<ReturnableWeaponProjectileBase>();

			_ccMovementController = GetComponentInParent<CCMovementController>();

			if (GrappleStart == null)
			{
				GrappleStart = transform;
			}
		}


	}
}