using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Project.HealthSystems
{
	public class Health : MonoBehaviour, IHealth
	{

		// Publics
		[Header("Health")]
		public float MaxHealth = 100f;

		public float CurrentHealth = 100f;

		[Header("Regeneration")]
		public bool HasRegeneration = false;

		public float RegenRate = 1f;

		public float RegenerationDelay = 5f;

		[Header("Temp Immortality")]
		public bool HasImmortalFrames = false;

		public float HowLongImmortalFramesLast = 1;

		[Header("Death Event (Runs only once)")]
		public UnityEvent OnDeath;

		// Privates
		private float _immortalFrameCounter = 0;

		private float _regenDelayCounter = 0;

		private bool _entityDied = false;


		void Start()
		{
			SetUpHealth();
		}

		void Update()
		{
			if (_entityDied) return;


			if (_regenDelayCounter >= 0) _regenDelayCounter -= Time.deltaTime;

			if (_regenDelayCounter <= 0 && CurrentHealth < MaxHealth && HasRegeneration)
			{
				CurrentHealth += Time.deltaTime * RegenRate;
			}


			if (_immortalFrameCounter >= 0) _immortalFrameCounter -= Time.deltaTime;


			if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;


			CheckIfDead();
		}

		private void CheckIfDead()
		{
			if (CurrentHealth <= 0)
			{
				OnDeath.Invoke();

				_entityDied = true;
			}
		}

		private void SetUpHealth()
		{
			CurrentHealth = MaxHealth;
		}

		void IHealth.HealHealth(float additionalHealth)
		{
			CurrentHealth += additionalHealth;
		}

		void IHealth.DamageHealth(float reductionAmmount)
		{
			if (_immortalFrameCounter > 0 && HasImmortalFrames) return;

			_regenDelayCounter = RegenerationDelay;

			CurrentHealth -= reductionAmmount;

			if (HasImmortalFrames) _immortalFrameCounter = HowLongImmortalFramesLast;

			// CheckIfDead();
		}

		void IHealth.ResetHealth()
		{
			CurrentHealth = MaxHealth;
		}

		void IHealth.SetHealth(float newHealth)
		{
			CurrentHealth = newHealth;
		}
	}
}
