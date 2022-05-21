using UnityEngine;

public class ProtagMovement : MonoBehaviour, IMoveable {
	[SerializeField] private Rigidbody rb;
	[SerializeField] private Entity entity;
	[Space]
	//[SerializeField] private float maxSpeed;
	[SerializeField] private float accel;
	[Space]
	[SerializeField] private float energyCost;

	public void Move(Vector3 direction) {
		rb.AddForce(direction * accel);

		entity.energyBar.currentValue -= direction.magnitude * energyCost;
		entity.energyBar.UpdateBar();
	}

	
}
