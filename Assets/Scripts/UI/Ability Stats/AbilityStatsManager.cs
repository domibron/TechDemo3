using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.AbilityStats
{
	public class AbilityStatsManager : MonoBehaviour
	{
		public static AbilityStatsManager Instance { get; private set; }

		[Header("Ability 1")]
		public Image AilityOneIcon;
		public Image AilityOneIconFill;
		public TMP_Text AilityOneName;

		[Header("Ability 2")]

		public Image AilityTwoIcon;
		public Image AilityTwoIconFill;
		public TMP_Text AilityTwoName;

		[Header("Ability 3")]

		public Image AilityThreeIcon;
		public Image AilityThreeIconFill;
		public TMP_Text AilityThreeName;



		void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this.gameObject);
			}
			else
			{
				Instance = this;
			}
		}

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void SetAbilityOne(string name, Sprite icon)
		{
			AilityOneIcon.sprite = icon;
			AilityOneName.text = name;
		}

		public void SetAbilityOneFill(float fill)
		{
			AilityOneIconFill.fillAmount = fill;
		}

		public void SetAbilityTwo(string name, Sprite icon)
		{
			AilityTwoIcon.sprite = icon;
			AilityTwoName.text = name;
		}

		public void SetAbilityTwoFill(float fill)
		{
			AilityTwoIconFill.fillAmount = fill;

		}

		public void SetAbilityThree(string name, Sprite icon)
		{
			AilityThreeIcon.sprite = icon;
			AilityThreeName.text = name;
		}

		public void SetAbilityThreeFill(float fill)
		{
			AilityThreeIconFill.fillAmount = fill;

		}
	}
}
