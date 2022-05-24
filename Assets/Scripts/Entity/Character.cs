using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity, IMoveable, IUseable
{
	public CharacterController CC => cc;
	[SerializeField] private CharacterController cc;
	[SerializeField] private Animator animator;

	[SerializeField] private float maxSpeed;
	[SerializeField] private float accel;
	public Vector3 velocity = Vector3.zero;

	float animSpeed = 0;

	public override void Reset()
	{
		base.Reset();
		animator = GetComponent<Animator>();
	}

	public override void Awake()
	{
		base.Awake();
	}

	public override void Start()
	{
		base.Start();
	}

	public virtual void Update()
	{
		animator.SetFloat("Speed", animSpeed, 0.1f, Time.deltaTime);
	}

	public override void Die()
	{
		base.Die();
	}

	public virtual void Move(Vector3 direction)
	{
		velocity = Vector3.MoveTowards(velocity, direction * maxSpeed, Time.deltaTime * accel);
		cc.Move(velocity * Time.deltaTime);
		//animator.SetFloat("Speed", velocity.magnitude);
		animSpeed = velocity.magnitude;
		if (direction.magnitude > 0.1f)
			Look(direction);
	}

	public virtual void Look(Vector3 direction, float rate = 720)
	{
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rate);
	}

	public virtual void Use() { }

	public virtual void Stop()
	{
		velocity = Vector3.zero;
		animSpeed = 0;
		//animator.SetFloat("Speed", 0);
	}
}