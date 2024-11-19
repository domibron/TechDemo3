using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.StatusEffects
{
	public interface IFreezable
	{
		public float FrozenPercentage { get; }

		public void Freeze(float percentageIncrease = 100f);

		public void UnFreeze();
	}
}
