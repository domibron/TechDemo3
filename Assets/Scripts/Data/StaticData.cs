using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
	public static class StaticData
	{
		public static int PLAYER_LAYER = 3;

		public static int LAYER_WITH_ONLY_PLAYER_IGNORED = ~(1 << StaticData.PLAYER_LAYER);
	}
}
