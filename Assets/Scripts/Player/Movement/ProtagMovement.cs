using UnityEngine;

public class ProtagMovement : MonoBehaviour, IMoveable
{
	[SerializeField] private Rigidbody rb;
	[SerializeField] private Entity entity;
	[SerializeField] private Energy energy;
	[Space]
	//[SerializeField] private float maxSpeed;
	[SerializeField] private float accel;
	[Space]
	[SerializeField, Tooltip("Units/sec @ Max Speed")] private float energyCost;

	public void Move(Vector3 direction)
	{
		if (energy.Use(direction.magnitude * energyCost * Time.deltaTime))
		{
			rb.AddForce(direction * accel);
		}
		
		entity.EnergyBar.UpdateBar(energy.Value / energy.MaxValue);
	}
}