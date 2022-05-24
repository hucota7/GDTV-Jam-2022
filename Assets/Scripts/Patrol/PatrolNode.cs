using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers.Vector;

[ExecuteInEditMode]
public class PatrolNode : MonoBehaviour
{
	[SerializeField] private PatrolInstruction[] instructions;

	public IEnumerator Process(Character character)
	{
		// instruct character to move towards node
		while (Vector2.Distance(character.transform.position.xz(), transform.position.xz()) > 0.1f)
		{
			character.Move((transform.position.xz() - character.transform.position.xz()).normalized.xz());
			yield return new WaitForEndOfFrame();
		}

		// if there are any instructions, process them in sequence
		if (instructions != null && instructions.Length > 0)
		{
			foreach (var instruction in instructions)
			{
				yield return instruction.Process(character);
			}
		}
	}
	
	private void OnTransformChildrenChanged()
	{
		instructions = GetComponentsInChildren<PatrolInstruction>();
	}
}
