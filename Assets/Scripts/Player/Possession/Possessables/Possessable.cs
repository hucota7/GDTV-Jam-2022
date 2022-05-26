using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Possessable : MonoBehaviour, IPossessable
{
	[SerializeField] protected Entity entity;
	public UnityEvent OnPossess, OnUnpossess;

	public bool IsPossessed { get; private set; }

	public Entity GetEntity()
	{
		return entity;
	}

	public virtual void Possess(IPossessable previouslyPossessed)
	{
		OnPossess.Invoke();
		IsPossessed = true;
		entity.SetOutline();
		//AudioManager.Play("PossessionSFX");
		//except when "possessing" the ghost
	}

	public virtual void Unpossess(IPossessable newlyPossessed)
	{
		OnUnpossess.Invoke();
		IsPossessed = false;
		entity.ClearOutline();
		//AudioManager.Play("UnpossessionSFX");
		//except when "unpossessing" the ghost
	}
}