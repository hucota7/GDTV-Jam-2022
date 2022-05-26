using UnityEngine;

public class GhostMovement : IMoveableConcreteImplementation
{
	[field: SerializeField] public CharacterController CC { get; private set; }
	[Space]
	[SerializeField] private float maxSpeed;
	[SerializeField] private float accel;

	[HideInInspector] public Vector3 velocity;

	public override void Move(Vector3 direction)
	{
		velocity = Vector3.MoveTowards(velocity, direction * maxSpeed, Time.deltaTime * accel);
		CC.Move(velocity * Time.deltaTime);
	}
}