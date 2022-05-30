using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : Entity, ICanPathfind, IUseable, IDragging
{
	[SerializeField] protected NavMeshAgent agent;
	[SerializeField] private Animator animator;
	[SerializeField] private Collider mainCollider;
	[SerializeField] private Possessable possessable;

	[Space]
	[SerializeField] private Transform draggingPoint;

	[Space]
	[SerializeField] private float maxSpeed;
	[SerializeField] private float speedModifier = 1f;
	[SerializeField] private float accel;
	[SerializeField, ReadOnly] private Vector3 velocity = Vector3.zero;

	public Transform keyHolder;
	[field: SerializeField] public bool HoldingKey { get; private set; } = false;
	[field: SerializeField] public bool IsDead { get; private set; } = false;

	public ThoughtBubble ThoughtBubble { get; private set; }
	public bool CanMove { get; protected set; } = true;
	protected float unblockMoveTime = -1;

	float animSpeed = 0;

	private IDraggable currentDraggable;

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
		if (IsDead)
		{
			Die();
			return;
		}

		base.Start();
		ThoughtBubble = Instantiate(InterfaceManager.Instance.thoughtBubblePrefab, InterfaceManager.Instance.worldCanvas.transform);
		ThoughtBubble.Init(uiPoint);
		agent.enabled = true;
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

	public virtual void FixedUpdate()
	{
		if (currentDraggable != null)
			currentDraggable.Drag(this);
	}

	public override void Die() {
		if (PossessionManager.Instance.CurrentPossessed == (IPossessable)possessable)
			PossessionManager.Instance.Possess();

		if (TryGetComponent(out Ragdoll ragdoll))
		{
			IsDead = true;
			agent.enabled = false;
			mainCollider.enabled = false;
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

	public virtual void PickupKey(Key key)
	{
		HoldingKey = true;
		Debug.Log($"{gameObject.name} picked up a key!");
	}

	public virtual void RemoveKey()
	{
		Destroy(keyHolder.GetComponentInChildren<Key>().gameObject);
		AudioManager.Play("UnlockSFX");
	}

	public virtual void Move(Vector3 direction)
	{
		if (CanMove)
		{
			velocity = Vector3.MoveTowards(velocity, direction * maxSpeed * speedModifier, Time.deltaTime * accel);

			agent.Move(velocity * Time.deltaTime);
			//animator.SetFloat("Speed", velocity.magnitude);
			animSpeed = velocity.magnitude;
			if (direction.magnitude > 0.1f)
				Look(direction);
		}
	}

	public void UpdateAnimationFromAgent()
	{
		animSpeed = agent.velocity.magnitude;
	}

	public bool TryPathfind(Vector3 worldPosition)
	{
		if (CanMove)
		{
			if (agent.SetDestination(worldPosition))
			{
				return true;
			}
			Debug.LogWarning("Couldn't get a path");
			Debug.DrawLine(transform.position, worldPosition, Color.red, 5);
		}
		return false;
	}

	public virtual void Look(Vector3 direction, float rate = 720)
	{
		if (CanMove)
		{
			if (currentDraggable != null)
				direction = -direction;

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


	public virtual void StartDragging(IDraggable dragged)
	{
		speedModifier = 0.5f;
		animator.SetBool("Dragging", true);
		currentDraggable = dragged;
	}

	public virtual void StopDragging(IDraggable dragged)
	{
		if (currentDraggable == null)
			return;

		speedModifier = 1f;
		animator.SetBool("Dragging", false);
		currentDraggable = null;
	}

	public Transform GetDragLinkPoint()
	{
		return draggingPoint;
	}
}