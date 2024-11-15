using System.Collections;
using System.Collections.Generic;
using Project.Health;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class MeleeWeapon : BaseWeapon
	{
		public MeleeWeaponSO meleeWeaponSO;

		// Privates
		private float _timeUntilNextAttack = 0;


		void Start()
		{
			SetUpWeapon();
		}

		void Update()
		{
			if (_timeUntilNextAttack >= 0)
			{
				_timeUntilNextAttack -= Time.deltaTime * meleeWeaponSO.FireRate;
			}
		}

		protected override void AimWeapon()
		{
			// Should Do with animations.
		}

		protected override void FireWeapon()
		{
			if (_timeUntilNextAttack <= 0)
			{
				_timeUntilNextAttack = 1f;

				if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, meleeWeaponSO.Range, StaticData.LAYER_WITH_ONLY_PLAYER_IGNORED))
				{
					hit.collider.GetComponent<IHealth>()?.RemoveHealth(meleeWeaponSO.Damage);
				}
			}
		}

		protected override void ReloadWeapon()
		{
			// inspect.
		}

		protected override void SetUpWeapon()
		{

		}
	}
}
