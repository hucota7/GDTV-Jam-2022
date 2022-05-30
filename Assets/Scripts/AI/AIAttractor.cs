using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttractor : MonoBehaviour
{
	public float viewableRadius = 5;

	private void FixedUpdate()
	{
		Collider[] hits = Physics.OverlapSphere(transform.position, viewableRadius, ~0, QueryTriggerInteraction.Collide);
		AIAttention ai;
		foreach (var hit in hits)
		{
			if (hit.TryGetComponent(out ai))
			{
				if (ai.IsVisible(transform.position))
				{
					ai.Attract(this);
				}
			}
		}
	}
}
