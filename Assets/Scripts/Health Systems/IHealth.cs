using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.HealthSystems
{
	public interface IHealth
	{
		public void SetHealth(float newHealth);

		public void HealHealth(float additionalHealth);
		public void DamageHealth(float reductionAmmount);

		public void ResetHealth();
	}
}
