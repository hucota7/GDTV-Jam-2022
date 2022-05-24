using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PatrolInstructions
{
	public class WaitInstruction : PatrolInstruction
	{
		[field: SerializeField] public float Duration { get; private set; }

		public override IEnumerator Process(Character character)
		{
			character.Stop();
			for (float t = 0; t < Duration; t += Time.deltaTime)
			{
				yield return new WaitForEndOfFrame();
			}
		}
	}
}