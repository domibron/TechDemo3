using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.StatusEffects
{
	public interface IKnockbackable
	{
		public void Addknockback(Vector3 force);
	}
}
