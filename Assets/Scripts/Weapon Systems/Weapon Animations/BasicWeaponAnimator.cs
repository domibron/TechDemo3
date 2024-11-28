using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	[RequireComponent(typeof(Animator))]
	public class BasicWeaponAnimator : WeaponAnimatorBase
	{
		public Animator WeaponAnimator;

		void Start()
		{
			if (WeaponAnimator == null)
			{
				WeaponAnimator.GetComponent<Animator>();
			}
		}


		public override void Aim(bool state)
		{
			WeaponAnimator.SetBool("Aiming", state);
		}

		public override void Fire(bool state)
		{
			WeaponAnimator.SetBool("Firing", state);
		}

		public override void Reload()
		{
			WeaponAnimator.SetTrigger("Reload");
		}

		public override void SpecialAction()
		{
			WeaponAnimator.SetTrigger("SpecialAction");
		}
	}
}
