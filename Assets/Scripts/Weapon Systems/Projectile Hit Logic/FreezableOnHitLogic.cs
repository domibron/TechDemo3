using System.Collections;
using System.Collections.Generic;
using Project.Gore;
using Project.HealthSystems;
using Project.StatusEffects;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class FreezableOnHitLogic : ProjectileHitLogicBase
	{
		public GoreType WeaponGoreType = GoreType.Nothing;

		[Range(0, 100)]
		public float MaxFrozenPercentage = 50f;

		public float FreezeIncreaseAmmount = 10f;

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public override bool HitThisObject(GameObject objectThatWasHit, float damageToObject)
		{
			if (objectThatWasHit == null) return false;

			if (objectThatWasHit.GetComponent<IGibs>() != null) objectThatWasHit.GetComponent<IGibs>().GibsGoreType = WeaponGoreType;

			print(objectThatWasHit.name);

			if (objectThatWasHit.GetComponent<IFreezable>() != null)
			{

				IFreezable freezable = objectThatWasHit.GetComponent<IFreezable>();

				float ammountToAdd = 0f;

				if (freezable.FrozenPercentage < MaxFrozenPercentage)
				{
					if (MaxFrozenPercentage - freezable.FrozenPercentage < FreezeIncreaseAmmount)
					{
						ammountToAdd = MaxFrozenPercentage - freezable.FrozenPercentage;
					}
					else
					{
						ammountToAdd = FreezeIncreaseAmmount;
					}
				}

				freezable.Freeze(ammountToAdd);

			}

			return true;
		}
	}
}
