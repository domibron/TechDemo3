using System.Collections;
using System.Collections.Generic;
using Project.Gore;
using Project.HealthSystems;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class StandardOnHitLogic : ProjectileHitLogicBase
	{
		public GoreType WeaponGoreType = GoreType.Nothing;

		/*
		Yes, its getting this bad, that sub, sub, sub, sub, sub systems are getting truned into modules.
		*/

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

			objectThatWasHit.GetComponent<IHealth>()?.DamageHealth(damageToObject);

			return true;
		}
	}
}
