using System.Collections;
using System.Collections.Generic;
using Project.HealthSystems;
using UnityEngine;

namespace Project.StatusEffects
{
	public class PosionableEntity : MonoBehaviour, IPosionable
	{
		public float PosionDamage = 10f;

		public float TickTime = 1f;

		[Tooltip("This does not effect tick time. it only ajust how fast posion debuff is remvoed.")]
		public float PosionTime = 10f;

		private float _posionPercentage = 0f;

		private IHealth health;

		private bool _isPosioned = false;

		private float _posionRemovalTime = 0f;

		private IFreezable freezable;

		private float _posionTickTime = 0f;

		void IPosionable.Posion(float posionPercentage)
		{
			_posionRemovalTime = PosionTime;
			_posionPercentage += posionPercentage;

			if (freezable == null) return;


			if (freezable.FrozenPercentage < 50f) // we dont want to reduce frozen stuff.
				freezable.Freeze(50f - freezable.FrozenPercentage);
		}

		void Start()
		{
			freezable = GetComponent<IFreezable>();
		}

		void Update()
		{
			if (_posionPercentage >= 100f)
			{
				_isPosioned = true;
			}

			if (_posionTickTime > 0)
			{
				_posionTickTime -= Time.deltaTime;
			}

			if (_posionRemovalTime > 0f)
			{
				_posionRemovalTime -= Time.deltaTime;
			}
			else
			{
				if (_isPosioned) return;

				_posionPercentage = 0f;
			}

			if (_isPosioned && _posionTickTime <= 0)
			{
				health.DamageHealth(PosionDamage);
			}
		}
	}
}
