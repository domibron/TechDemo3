using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.StatusEffects
{
	public interface IBurnable
	{
		public void SetOnFire(float duration);

		public void Extinguish();
	}
}
