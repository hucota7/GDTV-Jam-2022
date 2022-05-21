using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers.Vector
{
	public static class VectorUtil
	{
		public static Vector2 xz(this Vector3 v) => new Vector2(v.x, v.z);
		public static Vector3 xz(this Vector2 v) => new Vector3(v.x, 0, v.y);
	}
}
