using System.Collections;
using System.Collections.Generic;
using Project.Gore;
using Project.HealthSystems;
using Project.StatusEffects;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class StandardOnHitLogic : ProjectileHitLogicBase
	{
		public GoreType WeaponGoreType = GoreType.Nothing;

		public float StunDuration = 0f;

		public float KnockBackForce = 0f;

		/*
		Yes, its getting this bad, that sub, sub, sub, sub, sub systems are getting truned into modules.
		*/

		public override bool HitThisObject(GameObject objectThatWasHit, float damageToObject)
		{
			if (objectThatWasHit == null) return false;

			if (objectThatWasHit.GetComponent<IGibs>() != null) objectThatWasHit.GetComponent<IGibs>().GibsGoreType = WeaponGoreType;

			// now now, its just "tempoary" lol.

			objectThatWasHit.GetComponent<IFreezable>()?.UnFreeze();

			objectThatWasHit.GetComponent<IStunable>()?.Stun(StunDuration);

			objectThatWasHit.GetComponent<IKnockbackable>()?.Addknockback((objectThatWasHit.transform.position - transform.position).normalized * KnockBackForce);

			objectThatWasHit.GetComponent<IHealth>()?.DamageHealth(damageToObject);

			return true;
		}
	}
}
