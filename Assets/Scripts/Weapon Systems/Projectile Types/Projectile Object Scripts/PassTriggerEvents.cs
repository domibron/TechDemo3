using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	public class PassTriggerEvents : MonoBehaviour
	{
		public delegate void OnTriggerDelegate(GameObject targetObject);

		public event OnTriggerDelegate OnTriggerEnterEvent;
		public event OnTriggerDelegate OnTriggerExitEvent;

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		void OnTriggerEnter(Collider other)
		{
			if (OnTriggerEnterEvent != null) OnTriggerEnterEvent.Invoke(other.gameObject);
		}

		void OnTriggerExit(Collider other)
		{
			if (OnTriggerExitEvent != null) OnTriggerExitEvent.Invoke(other.gameObject);

		}
	}
}
