using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Possessable : MonoBehaviour, IPossessable
{
	[SerializeField] protected Entity entity;
	public UnityEvent OnPossess, OnUnpossess;

	public bool IsPossessed { get; private set; }
	public bool IsGettingDestroyed { get; private set; }

	public void MarkForDestruction() {
		IsGettingDestroyed = true;
	}

	public Entity GetEntity()
	{
		return entity;
	}

	public virtual void Possess(IPossessable previouslyPossessed)
	{
		OnPossess.Invoke();
		IsPossessed = true;
		entity.SetOutline();
	}

	public virtual void Unpossess(IPossessable newlyPossessed)
	{
		OnUnpossess.Invoke();
		IsPossessed = false;
		entity.ClearOutline();
	}
}