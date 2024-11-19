using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gore
{
	public enum GoreType
	{
		Ragdoll,
		ExploadWithGibs,
		Nothing,
	}

	public interface IGibs
	{
		public GoreType GibsGoreType { get; set; }

		public void BeginGibs(Vector3 startLocation);
		// public void BeginGibs(Vector3 startLocation, GoreType goreType);
	}
}
