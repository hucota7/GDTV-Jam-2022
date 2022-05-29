using UnityEngine;

public class RepelEffector : MonoBehaviour {
	[SerializeField] private float force;

	private void OnTriggerStay(Collider other) {
		Vector3 direction = other.transform.position - transform.position;
		direction.y = 0;

		other.attachedRigidbody.AddForce(direction * force);
	}
}
