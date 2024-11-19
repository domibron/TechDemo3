using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gore
{
	public class ShrinkGibOvertime : MonoBehaviour
	{
		public bool DelayShrink = true;

		public float Delay = 1f;

		public float ShrinkTime = 2f;

		// Start is called before the first frame update
		void Start()
		{
			StartCoroutine(ShrinkGib());
		}

		// Update is called once per frame
		void Update()
		{

		}

		IEnumerator ShrinkGib()
		{
			if (DelayShrink) yield return new WaitForSeconds(Delay);

			float time = 0;

			Vector3 scale = transform.localScale;

			while (time <= 1)
			{
				time += Time.deltaTime * (1 / ShrinkTime);

				transform.localScale = Vector3.Lerp(scale, Vector3.zero, time);

				yield return null;
			}
		}
	}
}