using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gore
{
	public class BasicGibs : MonoBehaviour, IGibs
	{
		public GameObject GibsToSpawnOnExpload;
		public GameObject GibsToSpawnOnRagdoll;

		private GoreType goreType = GoreType.Nothing;

		GoreType IGibs.GibsGoreType { get => goreType; set => goreType = value; }

		void IGibs.BeginGibs(Vector3 startLocation)
		{
			if (goreType == GoreType.ExploadWithGibs && GibsToSpawnOnExpload != null) Instantiate(GibsToSpawnOnExpload, startLocation, Quaternion.identity);
			else if (goreType == GoreType.Ragdoll && GibsToSpawnOnRagdoll != null) Instantiate(GibsToSpawnOnRagdoll, startLocation, Quaternion.LookRotation(transform.forward));
		}
	}
}
