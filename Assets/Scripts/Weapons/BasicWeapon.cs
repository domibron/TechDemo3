using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
	public class BasicWeapon : MonoBehaviour, IWeapon
	{
		public WeaponSOBase WeaponSO;

		void IWeapon.Aim()
		{
			throw new System.NotImplementedException();
		}

		void IWeapon.Fire()
		{
			throw new System.NotImplementedException();
		}

		void IWeapon.RPressed()
		{
			throw new System.NotImplementedException();
		}

		public virtual void SetUpWeapon()
		{

		}

	}
}
