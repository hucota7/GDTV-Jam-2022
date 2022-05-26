using UnityEngine;

public class ProtagMovement : IMoveableConcreteImplementation {
	[field: SerializeField] public CharacterController CC { get; private set; }
	[SerializeField] private Entity entity;
	[SerializeField] private Energy energy;
	[Space]
	[SerializeField] private float maxSpeed;
	[SerializeField] private float accel;
	[Space]
	[SerializeField, Tooltip("Units/sec @ Max Speed")] private float energyCost;

	private Vector3 velocity;

	public override void Move(Vector3 direction)
	{
		velocity = Vector3.MoveTowards(velocity, direction * maxSpeed, Time.deltaTime * accel);
		
		if (energy.Use(direction.magnitude * energyCost * Time.deltaTime)) {
			CC.Move(velocity * Time.deltaTime);
		}
		
		entity.EnergyBar.UpdateBar(energy.Value / energy.MaxValue);
	}
}