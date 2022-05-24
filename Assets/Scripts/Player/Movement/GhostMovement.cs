using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour, IMoveable
{
	public CharacterController CC => cc;
	[SerializeField] private CharacterController cc;
	[Space]
	[SerializeField] private float maxSpeed;
	[SerializeField] private float accel;

	public Vector3 velocity = Vector3.zero;

	public void Move(Vector3 direction)
	{
		velocity = Vector3.MoveTowards(velocity, direction * maxSpeed, Time.deltaTime * accel);
		cc.Move(velocity * Time.deltaTime);
	}
}