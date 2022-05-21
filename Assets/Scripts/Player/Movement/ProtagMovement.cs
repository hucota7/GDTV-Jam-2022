using UnityEngine;

public class ProtagMovement : MonoBehaviour, IMoveable {
	[SerializeField] private Rigidbody rb;
	[Space]
	//[SerializeField] private float maxSpeed;
	[SerializeField] private float accel;

	public void Move(Vector3 direction) {
		rb.AddForce(direction * accel);
	}

	
}
