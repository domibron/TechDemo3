using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public enum ArcType
	{
		Horizontal,
		Vertical,
	}

	public class ArcProjectile : WeaponProjectileBase
	{
		public static float HALF_OF_PI = 1.57079632679f;

		public ArcType ProjectileArcDirection;

		public float ArcMaxSpread = .1f;

		public int BulletAmmount = 5;

		[Tooltip("This will make bullet ammount odd and greater than 2!")]
		public bool CentreBullet = true;

		private ProjectileHitLogicBase hitLogic;

		void Start()
		{
			hitLogic = GetComponent<ProjectileHitLogicBase>();

			if (hitLogic == null)
			{
				throw new NullReferenceException("Cannot work with no logic, please add something with " + nameof(ProjectileHitLogicBase) + " to this object!");
			}
		}

		public override void StartFireProjectile(float damage, float range)
		{
			int bulletTotal = BulletAmmount;

			RaycastHit hit;

			if (CentreBullet)
			{
				if (bulletTotal < 3) bulletTotal = 3;
				else if (bulletTotal % 2 == 0) bulletTotal++;

				if (Physics.Raycast(transform.position, transform.forward, out hit, range, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
				{
					hitLogic.HitThisObject(hit.collider.gameObject, damage);
				}

				Debug.DrawRay(transform.position, transform.forward * range, Color.red, 10);

				bulletTotal--;
			}

			float randomPoint;
			Vector3 rayDirection;



			// We get the end of the arc raycasts.


			if (ProjectileArcDirection == ArcType.Horizontal) randomPoint = GetRadArc(ArcMaxSpread, 0);
			else randomPoint = GetRadArc(ArcMaxSpread, 0);

			rayDirection = transform.forward + transform.right * randomPoint;
			// rayDirection = transform.forward;
			// rayDirection += randomPoint;

			if (Physics.Raycast(transform.position, rayDirection, out hit, range, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
			{
				hitLogic.HitThisObject(hit.collider.gameObject, damage);


			}

			Debug.DrawRay(transform.position, rayDirection * range, Color.blue, 10);


			if (ProjectileArcDirection == ArcType.Horizontal) randomPoint = GetRadArc(ArcMaxSpread, 1);
			else randomPoint = GetRadArc(ArcMaxSpread, 1);

			rayDirection = transform.forward + transform.right * randomPoint;
			// rayDirection = transform.forward;
			// rayDirection += randomPoint;

			if (Physics.Raycast(transform.position, rayDirection, out hit, range, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
			{
				hitLogic.HitThisObject(hit.collider.gameObject, damage);


			}


			Debug.DrawRay(transform.position, rayDirection * range, Color.blue, 10);

			bulletTotal -= 2;

			Vector3 vec = Quaternion.AngleAxis(5f, transform.up) * transform.forward;
			Vector3 vec2 = Quaternion.AngleAxis(-5f, transform.up) * transform.forward;

			Debug.DrawRay(transform.position, vec, Color.yellow, 20f);
			Debug.DrawRay(transform.position, vec2, Color.yellow, 20f);

			if (bulletTotal <= 0) return;


			float percentageDist = 1f / (bulletTotal + 1);

			for (int i = 1; i <= bulletTotal; i++)
			{

				if (ProjectileArcDirection == ArcType.Horizontal) randomPoint = GetRadArc(ArcMaxSpread, percentageDist * i);
				else randomPoint = GetRadArc(ArcMaxSpread, percentageDist * i);


				rayDirection = transform.forward + transform.right * randomPoint;
				// rayDirection = transform.forward;
				// rayDirection += randomPoint;

				//print($"{randomPoint} | {transform.forward} = {rayDirection}");

				//raycast
				if (Physics.Raycast(transform.position, rayDirection, out hit, range, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
				{
					hitLogic.HitThisObject(hit.collider.gameObject, damage);


				}


				Debug.DrawRay(transform.position, rayDirection * range, Color.green, 10);
			}
		}

		//Somthing fucked up, increasing spread angle cuases wierd math were angle is not treated like a angle of spread. maybe with the lerp math? Or end calcs?

		float GetRadArc(float spreadAt1m, float anglePercentage)
		{
			float radPercentage = Mathf.Lerp(-HALF_OF_PI, HALF_OF_PI, anglePercentage);

			return Mathf.Sin(radPercentage) * spreadAt1m;
		}

		// Vector3 GetVerticalArcOffset(float spreadAt1m, float anglePercentage)
		// {
		// 	float radPercentage = Mathf.Lerp(-HALF_OF_PI, HALF_OF_PI, anglePercentage);

		// 	return new Vector3(0, Mathf.Sin(radPercentage) * spreadAt1m, 0);
		// }
		/*
		I hate math :c i hate 3d space and directions and rotations, this is immence suffering.
		*/

		public override void EndFireProjectile()
		{

		}
	}
}
