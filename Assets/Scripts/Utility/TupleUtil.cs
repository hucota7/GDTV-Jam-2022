using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers.Tuple
{
	public static class TupleUtil
	{
		public static (float, float) Tuple(this Vector2 vec) => (vec.x, vec.y);
		public static (float, float, float) Tuple(this Vector3 vec) => (vec.x, vec.y, vec.z);
	}
}