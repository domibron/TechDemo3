using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Health
{
	public interface IHealth
	{
		public void SetHealth(float newHealth);

		public void AddHealth(float additionalHealth);
		public void RemoveHealth(float reductionAmmount);

		public void ResetHealth();
	}
}
