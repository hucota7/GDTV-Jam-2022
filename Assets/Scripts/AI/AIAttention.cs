using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class AIAttention : MonoBehaviour
{
	private Character character;

	public LayerMask obstructionMask;
	public float viewAngle;

	[ReadOnly] public AIAttractor attraction = null;

	private void Awake()
	{
		character = GetComponent<Character>();
	}

	private void FixedUpdate()
	{
		if (attraction && !IsVisible(attraction.transform.position))
		{

		}
	}

	public bool IsVisible(Vector3 pos)
	{
		Vector3 locPos = transform.InverseTransformPoint(pos);
		if (Mathf.Abs(Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg) <= viewAngle)
		{
			if (Physics.Raycast(transform.position, pos - transform.position, out RaycastHit hit, (pos - transform.position).magnitude, obstructionMask))
			{
				Debug.DrawLine(transform.position, hit.point, Color.red);
			}
			else
			{
				Debug.DrawLine(transform.position, pos, Color.green);
				return true;
			}
		}
		else
		{
			Debug.DrawLine(transform.position, pos, Color.red);
		}
		return false;
	}

	public void Attract(AIAttractor attractor)
	{
		attraction = attractor;
		character.TryPathfind(attraction.transform.position);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawFrustum(transform.position, viewAngle, 5, 0, 1);
	}
}
