using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
	public class SetObjectActive : MonoBehaviour
	{
		public GameObject TargetGameObject;

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void SetActiveFalse()
		{
			TargetGameObject.SetActive(false);
		}

		public void SetActiveTrue()
		{
			TargetGameObject.SetActive(true);
		}

	}
}
