using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Project.HealthSystems;

namespace Project.UI.HealthUI
{
	public class DamageVignette : MonoBehaviour
	{
		public Health Health;

		public Image DamageVignetteImage;

		[Range(0, 255)]
		public float MaxTransparancy = 100f;
		[Range(0, 255)]
		public float MinTransparancy = 50f;

		[Range(0f, 1f)]
		public float PercentageOfHealthToShowDamage = 0.25f;

		public float BlinkRate = 5;
		public float FadeInRate = 1;
		public float FadeOutRate = 1;

		private Color _color;

		private bool _blinking = false;

		void Start()
		{
			_color = DamageVignetteImage.color;
			DamageVignetteImage.enabled = false;
		}

		// public float
		void Update()
		{
			if (Health.CurrentHealth < Health.MaxHealth * PercentageOfHealthToShowDamage && !_blinking)
			{
				StartCoroutine(StartBlinking());
			}

		}

		IEnumerator StartBlinking()
		{
			_blinking = true;


			float BlinkTime = 0f;
			bool IsLerpDown = false;

			Color color = _color;
			color.a = 0;


			DamageVignetteImage.enabled = true;


			while (color.a < MinTransparancy / 255f)
			{
				BlinkTime += Time.deltaTime * FadeInRate;
				color.a = Mathf.Lerp(0, MaxTransparancy / 255f, BlinkTime);
				DamageVignetteImage.color = color;
				yield return null;
			}


			while (Health.CurrentHealth < Health.MaxHealth * PercentageOfHealthToShowDamage)
			{
				if (IsLerpDown)
				{
					BlinkTime -= Time.deltaTime * BlinkRate;
				}
				else
				{
					BlinkTime += Time.deltaTime * BlinkRate;
				}

				if (BlinkTime >= 1) IsLerpDown = true;
				else if (BlinkTime <= 0) IsLerpDown = false;


				color.a = Mathf.Lerp(MinTransparancy / 255f, MaxTransparancy / 255f, BlinkTime);

				DamageVignetteImage.color = color;

				yield return null;
			}

			float curretTransparancy = color.a;


			while (color.a > 0)
			{
				BlinkTime -= Time.deltaTime * FadeOutRate;
				color.a = Mathf.Lerp(0, curretTransparancy, BlinkTime);
				DamageVignetteImage.color = color;
				yield return null;
			}

			DamageVignetteImage.color = _color;
			DamageVignetteImage.enabled = false;

			_blinking = false;
		}


	}


}
