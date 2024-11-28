using System.Collections;
using System.Collections.Generic;
using Project.InputHandling;
using Project.UI.AbilityStats;
using UnityEngine;

namespace Project.Abilities
{
	public class AbilityManager : MonoBehaviour
	{


		public AbilityBase AbilitySlot1;
		public AbilityBase AbilitySlot2;
		public AbilityBase AbilitySlot3;

		private PlayerInputHandler _inputHandler;

		void Start()
		{

			_inputHandler = GetComponent<PlayerInputHandler>();


			if (AbilitySlot1 != null)
			{
				AbilityStatsManager.Instance.SetAbilityOne(AbilitySlot1.AbilitySO.Name, AbilitySlot1.AbilitySO.Icon);
			}

			if (AbilitySlot2 != null)
			{
				AbilityStatsManager.Instance.SetAbilityTwo(AbilitySlot2.AbilitySO.Name, AbilitySlot2.AbilitySO.Icon);
			}

			if (AbilitySlot3 != null)
			{
				AbilityStatsManager.Instance.SetAbilityThree(AbilitySlot3.AbilitySO.Name, AbilitySlot3.AbilitySO.Icon);
			}

		}

		// Update is called once per frame
		void Update()
		{
			if (_inputHandler.GetKeyDown(_inputHandler.Ablility1Key))
			{
				if (AbilitySlot1 != null)
				{
					AbilitySlot1.ActivateAbility();
				}
			}



			if (_inputHandler.GetKeyDown(_inputHandler.Ablility2Key))
			{
				if (AbilitySlot2 != null)
				{
					AbilitySlot2.ActivateAbility();
				}
			}



			if (_inputHandler.GetKeyDown(_inputHandler.Ablility3Key))
			{
				if (AbilitySlot3 != null)
				{
					AbilitySlot3.ActivateAbility();
				}
			}


			if (AbilityStatsManager.Instance == null)
			{
				Debug.LogError("AbilityStatsManager null!");
				return;
			}


			if (AbilitySlot1 != null)
			{
				AbilityStatsManager.Instance.SetAbilityOneFill(AbilitySlot1._currentCoolDownTime / AbilitySlot1.AbilitySO.CoolDown);
			}

			if (AbilitySlot2 != null)
			{
				AbilityStatsManager.Instance.SetAbilityTwoFill(AbilitySlot2._currentCoolDownTime / AbilitySlot2.AbilitySO.CoolDown);
			}

			if (AbilitySlot3 != null)
			{
				AbilityStatsManager.Instance.SetAbilityThreeFill(AbilitySlot3._currentCoolDownTime / AbilitySlot3.AbilitySO.CoolDown);
			}
		}
	}
}
