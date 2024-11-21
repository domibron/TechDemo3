using System.Collections;
using System.Collections.Generic;
using Project.StatusEffects;
using UnityEngine;

namespace Project.Abilities
{
	public class CryoBlastAbility : AbilityBase
	{
		public float Radius = 5f;

		public override void ActivateAbility()
		{
			if (_currentCoolDownTime > 0f) return;

			Collider[] hits = Physics.OverlapSphere(transform.position, Radius, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS);

			foreach (Collider hit in hits)
			{
				if (hit.GetComponent<IFreezable>() == null) continue;

				hit.GetComponent<IFreezable>().Freeze(100f);
			}

			_currentCoolDownTime = AbilitySO.CoolDown;
		}

		void Update()
		{
			if (_currentCoolDownTime > 0f)
			{
				_currentCoolDownTime -= Time.deltaTime;
			}
		}
	}
}
