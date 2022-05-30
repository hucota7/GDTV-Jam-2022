using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorHole : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		Ragdoll ragdoll = other.GetComponentInChildren<Ragdoll>();

		if (ragdoll != null) {

			int layer = LayerMask.NameToLayer("FallThru");
			foreach (var c in ragdoll.Colliders) {
				c.gameObject.layer = layer;
			}

			if (other.TryGetComponent(out Character ch)) {
				ch.Die();
			}
			else {
				ragdoll.ActivateRagdoll();
			}
		}
		else if (other.GetComponent<Rat>() != null) {
			other.GetComponent<Rat>().Die();
		}
	}
}
