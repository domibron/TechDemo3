using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gore
{
	public class RemoveGibAfterTime : MonoBehaviour
	{
		public float TimeBeforeRemoval = 5f;

		// Start is called before the first frame update
		void Start()
		{
			Destroy(gameObject, TimeBeforeRemoval);
		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}
