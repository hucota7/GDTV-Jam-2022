using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PatrolInstructions
{
	public class LookInstruction : PatrolInstruction
	{
		public float lookRate = 360;

		public override IEnumerator Process(Character character)
		{
			character.Stop();
			while (character.transform.forward != transform.forward)
			{
				character.Look(transform.forward, lookRate);
				yield return new WaitForEndOfFrame();
			}
		}
	}
}