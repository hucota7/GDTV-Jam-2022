using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers.Array
{
	public static class ArrayUtil
	{
		public static void ForEach<T>(this T[] array, System.Action<T> action)
		{
			foreach (T element in array) { action.Invoke(element); }
		}

		public static int RandomIndex<T>(this T[] array)
		{
			return Random.Range(0, array.Length);
		}

		public static T RandomElement<T>(this T[] array)
		{
			return array[array.RandomIndex()];
		}

		public static T At<T>(this T[] array, int index)
		{
			int mod(int a, int b) => b * (int)(1f - (float)a / (float)b) + a;
			return array[mod(index, array.Length)];
		}
	}
}