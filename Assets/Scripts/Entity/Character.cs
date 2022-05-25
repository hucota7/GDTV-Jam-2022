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
	[SerializeField, ReadOnly] Vector3 velocity = Vector3.zero;

	public ThoughtBubble ThoughtBubble { get; private set; }
	public bool CanMove { get; protected set; } = true;
	protected float unblockMoveTime = -1;

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
		ThoughtBubble = Instantiate(InterfaceManager.Instance.thoughtBubblePrefab, InterfaceManager.Instance.worldCanvas.transform);
		ThoughtBubble.Init(uiPoint);
	}

	public virtual void Update()
	{
		animator.SetFloat("Speed", animSpeed, 0.1f, Time.deltaTime);

		if (unblockMoveTime != -1)
		{
			if (unblockMoveTime - Time.time <= 0)
			{
				UnblockMovement();
			}
		}
	}

	public override void Die()
	{
		if (TryGetComponent(out Ragdoll ragdoll))
		{
			ragdoll.ActivateRagdoll();
			StopAllCoroutines();
			CanMove = false;
			velocity = Vector3.zero;
			enabled = false;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public virtual void Move(Vector3 direction)
	{
		if (CanMove)
		{
			velocity = Vector3.MoveTowards(velocity, direction * maxSpeed, Time.deltaTime * accel);
			cc.Move(velocity * Time.deltaTime);
			//animator.SetFloat("Speed", velocity.magnitude);
			animSpeed = velocity.magnitude;
			if (direction.magnitude > 0.1f)
				Look(direction);
		}
	}

	public virtual void Look(Vector3 direction, float rate = 720)
	{
		if (CanMove)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rate);
		}
	}

	public virtual void Use() { }

	public virtual void Stop()
	{
		velocity = Vector3.zero;
		animSpeed = 0;
		//animator.SetFloat("Speed", 0);
	}

	public virtual void BlockMovement(float duration)
	{
		if (unblockMoveTime == -1 || Time.time - unblockMoveTime < duration)
		{
			CanMove = false;
			unblockMoveTime = Time.time + duration;
		}
	}

	public virtual void UnblockMovement()
	{
		CanMove = true;
		unblockMoveTime = -1;
	}

	public virtual void OnPossessed()
	{
		Stop();
		BlockMovement(3.5f);
		animator.SetTrigger("Possessed");
	}

	public virtual void OnUnpossessed()
	{
		Stop();
		BlockMovement(3.5f);
		ThoughtBubble.Question();
		animator.SetTrigger("Unpossessed");
	}
}