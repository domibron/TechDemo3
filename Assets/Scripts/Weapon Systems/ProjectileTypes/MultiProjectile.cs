using System.Collections;
using System.Collections.Generic;
using Project.HealthSystems;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class MultiProjectile : MonoBehaviour, IWeaponProjectile
	{
		public int ProjectileAmmount = 8;

		public float ProjectileSpread = 0.1f;

		public bool CentreProjectile = true;

		/// <summary>
		/// Generates the rays to hit entities.
		/// </summary>
		/// <param name="damage">Damager per line / pellet.</param>
		/// <param name="range">The range of the raycast</param>
		void IWeaponProjectile.FireProjectile(float damage, float range)
		{

			// centre Bullet
			int BulletAmmount = ProjectileAmmount;

			if (CentreProjectile)
			{
				if (Physics.Raycast(transform.position, transform.forward, out RaycastHit CentreHit, range, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
				{
					CentreHit.collider.gameObject.GetComponent<IHealth>()?.DamageHealth(damage);
				}

				Debug.DrawRay(transform.position, transform.forward * range, Color.red, 10);

				BulletAmmount--;
			}

			Vector3 randomPoint;
			Vector3 rayDirection;

			float percentageDist = 1f / BulletAmmount;


			for (int i = 0; i <= BulletAmmount; ++i)
			{
				//create the ray with offset based around the sircle. add some noise for extra flair.
				/*
				What the GetOffsetAlongAImaginaryCircle generates when used correctly:
												
												   o 
											o              o 
											
											 
										  o                  o
										  
										  
											o              o 		"Urm, thats a square" - we are using minecraft as a example.	
												   o 
												   
				Yes, its a 2d hollow circle. You plug in a percentage and it will give you the point.
				"Why use this rather than just random points like other games" because you're wrong!
				This allows extra precition. Shotcons follow a pattern not a spray, the spray is when the pellet gets infuence.
				Hence you can add some nouse or random, but the shape is maintained but also reliable.
				This removes the fucking random chance in game attempting to do shotguns.
				I like to hit my shots, this is why I added one in the middle. this is a multi angle weapon.
				
				~ Random chance sucks, especially with skill based games.
				
				If you read this, have a cookie 🍪
				*/

				// Damage is per pellet. O.O

				randomPoint = GetOffsetAlongAImaginaryCircle(ProjectileSpread, percentageDist * i);
				rayDirection = transform.forward;
				rayDirection += randomPoint;

				//raycast
				if (Physics.Raycast(transform.position, rayDirection, out RaycastHit hit, range, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS))
				{
					hit.collider.gameObject.GetComponent<IHealth>()?.DamageHealth(damage);
				}


				Debug.DrawRay(transform.position, rayDirection * range, Color.green, 10);


			}
		}

		private Vector3 GetOffsetAlongAImaginaryCircle(float radius, float percentInDecimal)
		{
			// what the... the math... its.... its... pain. How I figured this out, i dont even know.
			float angleAsRad = Mathf.Lerp(0f, 6.28318530718f, percentInDecimal);

			// thinking about the imprecision of computers and making them do precise calcuations with floats scare me
			return new Vector3(Mathf.Cos(angleAsRad) * radius, Mathf.Sin(angleAsRad) * radius);
		}
	}
}
