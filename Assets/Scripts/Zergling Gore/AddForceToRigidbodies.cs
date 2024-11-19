using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gore
{
	public class AddForceToRigidbodies : MonoBehaviour
	{
		public float Force = 10f;

		public bool AddSomeRandomToDir = true;

		public float RandomMinMax = 1f;

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		Vector3 GetRandomeVector(float randomMinMax = 1)
		{
			return new Vector3(Random.Range(-randomMinMax, randomMinMax), Random.Range(-randomMinMax, randomMinMax), Random.Range(-randomMinMax, randomMinMax));
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.GetComponent<Rigidbody>() != null)
			{

				other.attachedRigidbody.AddForce(((other.transform.position - transform.position).normalized * Force) + GetRandomeVector(RandomMinMax), ForceMode.Impulse);
			}
		}
	}
}
