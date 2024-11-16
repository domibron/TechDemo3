using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
	public static class StaticData
	{
		public static int PLAYER_LAYER = 3;

		public static int WEAPON_LAYER = 6;

		public static int LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS = ~((1 << StaticData.PLAYER_LAYER) | (1 << StaticData.WEAPON_LAYER));
	}
}
