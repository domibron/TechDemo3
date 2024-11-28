using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.HealthSystems;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class LightningChainWeaponProjectile : WeaponProjectileBase
	{
		[Range(0f, 1f)]
		public float DamageDropOffRatePercentage = 0.15f;

		public float MaxArcReachRadius = 5f;

		public int MaxEntitiesHitPerArc = 3;

		public LayerMask AllowedLayers;

		[SerializeField]
		private ProjectileHitLogicBase hitLogic;

		private List<GameObject> _entitiesZapped = new List<GameObject>();

		float _damage;


		void Start()
		{
			hitLogic = GetComponent<ProjectileHitLogicBase>();
		}

		public override void EndFireProjectile()
		{

		}

		public override void StartFireProjectile(float damage, float range)
		{
			_damage = damage;

			if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range, AllowedLayers))
			{
				StartCoroutine(ZapEntities(hit.collider.gameObject));
			}

		}

		IEnumerator ZapEntities(GameObject startObject)
		{


			if (startObject.GetComponent<IHealth>() != null)
			{
				List<GameObject> newZappedItems = new List<GameObject>();
				Collider[] potentialItems;
				float _newDamage = _damage;

				_entitiesZapped.Add(startObject);
				newZappedItems.Add(startObject);



				while (newZappedItems.Count > 0)
				{


					List<GameObject> currentlyZappedItems = newZappedItems.ToList();
					newZappedItems.Clear();

					print(currentlyZappedItems.Count);

					if (currentlyZappedItems.Count > 0)
					{
						foreach (GameObject item in currentlyZappedItems)
						{
							if (item == null) continue;
							potentialItems = Physics.OverlapSphere(item.transform.position, MaxArcReachRadius, AllowedLayers);

							if (potentialItems.Length > 0)
							{
								int count = 0;

								foreach (Collider pitem in potentialItems)
								{
									if (_entitiesZapped.Contains(pitem.gameObject)) continue;

									newZappedItems.Add(pitem.gameObject);
									_entitiesZapped.Add(pitem.gameObject);


									count++;
									if (count >= MaxEntitiesHitPerArc) break;
								}
							}
						}
					}

					foreach (GameObject item in currentlyZappedItems)
					{
						hitLogic.HitThisObject(item.gameObject, _newDamage);
					}

					_newDamage -= _newDamage * DamageDropOffRatePercentage;


					yield return null;
				}

				print("END");


			}

			_entitiesZapped.Clear();
		}
	}
}
