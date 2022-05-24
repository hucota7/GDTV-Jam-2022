using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PatrolRoute : MonoBehaviour
{
	public EndMode endMode = EndMode.Loop;
	[SerializeField] private PatrolNode[] nodes;

	public IEnumerator Process(Character character)
	{
		bool pong = false;
		int i = 0;
		// TODO: set i as the nearest node to character
		for(;;)
		{
			yield return nodes[i].Process(character);

			if (pong)
			{
				i--;
				if (i == 0) pong = false;
			}
			else
			{
				i++;
				if (endMode == EndMode.PingPong && i == nodes.Length - 1)
				{
					pong = true;
				}
				else if (endMode == EndMode.Loop && i > nodes.Length)
				{
					i = 0;
				}
				else if (endMode == EndMode.Stop && i > nodes.Length)
				{
					break;
				}
			}
		}
	}

	private void OnTransformChildrenChanged()
	{
		nodes = GetComponentsInChildren<PatrolNode>();
	}

	public enum EndMode { Stop, Loop, PingPong }
}
