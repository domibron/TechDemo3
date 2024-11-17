using System.Collections;
using System.Collections.Generic;
using Project.HealthSystems;
using UnityEngine;

namespace Project.WeaponSystems
{
	public enum ArcType
	{
		Horizontal,
		Vertical,
	}

	public class ArcProjectile : MonoBehaviour, IWeaponProjectile
	{
		public static float HALF_OF_PI = 1.57079632679f;

		public ArcType ProjectileArcDirection;

		public float ArcMaxSpread = .1f;

		public int BulletAmmount = 5;

		[Tooltip("This will make bullet ammount odd and greater than 2!")]
		public bool CentreBullet = true;

		void Update()
		{
			print(transform.name);

			Debug.DrawRay(transform.position, transform.forward, Color.green);

			Debug.DrawRay(transform.position, new Vector3(transform.forward.x * 0.5f, transform.forward.y, transform.forward.z * 0.5f), Color.red);
		}

		void IWeaponProjectile.FireProjectile(float damage, float range)
		{
			int bulletTotal = BulletAmmount;

			if (CentreBullet)
			{
				if (bulletTotal < 3) bulletTotal = 3;
				else if (bulletTotal % 2 == 0) bulletTotal++;

				if (Physics.Raycast(transform.position, transform.forward, out RaycastHit centreHit, range, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
				{
					centreHit.collider.gameObject.GetComponent<IHealth>()?.DamageHealth(damage);
				}

				Debug.DrawRay(transform.position, transform.forward * range, Color.red, 10);

				bulletTotal--;
			}

			float randomPoint;
			Vector3 rayDirection;

			RaycastHit endHit;

			// We get the end of the arc raycasts.


			if (ProjectileArcDirection == ArcType.Horizontal) randomPoint = GetRadArc(ArcMaxSpread, 0);
			else randomPoint = GetRadArc(ArcMaxSpread, 0);

			rayDirection = transform.forward + transform.right * randomPoint;
			// rayDirection = transform.forward;
			// rayDirection += randomPoint;

			if (Physics.Raycast(transform.position, rayDirection, out endHit, range, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
			{
				endHit.collider.gameObject.GetComponent<IHealth>()?.DamageHealth(damage);
			}

			Debug.DrawRay(transform.position, rayDirection * range, Color.blue, 10);


			if (ProjectileArcDirection == ArcType.Horizontal) randomPoint = GetRadArc(ArcMaxSpread, 1);
			else randomPoint = GetRadArc(ArcMaxSpread, 1);

			rayDirection = transform.forward + transform.right * randomPoint;
			// rayDirection = transform.forward;
			// rayDirection += randomPoint;

			if (Physics.Raycast(transform.position, rayDirection, out endHit, range, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
			{
				endHit.collider.gameObject.GetComponent<IHealth>()?.DamageHealth(damage);
			}


			Debug.DrawRay(transform.position, rayDirection * range, Color.blue, 10);

			bulletTotal -= 2;



			if (bulletTotal <= 0) return;


			float percentageDist = 1f / (bulletTotal + 1);

			for (int i = 1; i <= bulletTotal; i++)
			{

				if (ProjectileArcDirection == ArcType.Horizontal) randomPoint = GetRadArc(ArcMaxSpread, percentageDist * i);
				else randomPoint = GetRadArc(ArcMaxSpread, percentageDist * i);


				rayDirection = transform.forward + transform.right * randomPoint;
				// rayDirection = transform.forward;
				// rayDirection += randomPoint;

				print($"{randomPoint} | {transform.forward} = {rayDirection}");

				//raycast
				if (Physics.Raycast(transform.position, rayDirection, out RaycastHit hit, range, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
				{
					hit.collider.gameObject.GetComponent<IHealth>()?.DamageHealth(damage);
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
	}
}
