using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	[RequireComponent(typeof(LineRenderer))]
	public class BeamWeaponProjectile : WeaponProjectileBase
	{
		public Transform StartPoint;

		[SerializeField]
		private ProjectileHitLogicBase _hitLogic;

		private LineRenderer _lineRenderer;

		private bool _fireBeam = false;

		private RaycastHit _hit;

		private float _range;

		void Start()
		{
			_lineRenderer = GetComponent<LineRenderer>();

			if (StartPoint == null)
			{
				StartPoint = transform;
			}
		}

		public override void StartFireProjectile(float damage, float range)
		{
			_fireBeam = true;

			_range = range;


			if (Physics.Raycast(transform.position, transform.forward * range, out RaycastHit hit, ~StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
			{
				_hitLogic.HitThisObject(hit.collider.gameObject, damage);
			}
		}

		public override void EndFireProjectile()
		{
			_fireBeam = false;




		}

		void Update()
		{
			if (_fireBeam)
			{
				Vector3 endPoint;

				if (Physics.Raycast(transform.position, transform.forward * _range, out RaycastHit hit, ~StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
				{
					endPoint = hit.point;
				}
				else
				{
					endPoint = transform.position + transform.forward * _range;
				}


				_lineRenderer.SetPosition(0, StartPoint.position);
				_lineRenderer.SetPosition(1, transform.position + transform.forward * _range);


				_lineRenderer.enabled = true;
			}
			else
			{
				_lineRenderer.enabled = false;
			}
		}


	}
}
