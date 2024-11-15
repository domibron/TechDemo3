using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Health
{
	public class Health : MonoBehaviour, IHealth
	{

		// Publics
		public float MaxHealth = 100f;

		public float CurrentHealth = 100f;

		public float RegenRate = 1f;

		public UnityEvent OnDeath;

		// Privates


		void Start()
		{
			SetUpHealth();
		}

		void Update()
		{
			if (CurrentHealth <= 0)
			{
				OnDeath.Invoke();
			}
		}

		private void SetUpHealth()
		{
			CurrentHealth = MaxHealth;
		}

		void IHealth.AddHealth(float additionalHealth)
		{
			CurrentHealth += additionalHealth;
		}

		void IHealth.RemoveHealth(float reductionAmmount)
		{
			CurrentHealth -= reductionAmmount;
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
