using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour, IMoveable
{
	[field: SerializeField] public CharacterController CC { get; private set; }
	[Space]
	[SerializeField] private float maxSpeed;
	[SerializeField] private float accel;

	[HideInInspector] public Vector3 velocity;

	public void Move(Vector3 direction)
	{
		velocity = Vector3.MoveTowards(velocity, direction * maxSpeed, Time.deltaTime * accel);
		CC.Move(velocity * Time.deltaTime);
	}
}