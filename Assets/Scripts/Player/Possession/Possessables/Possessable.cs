using UnityEngine;

public class Possessable : MonoBehaviour, IPossessable
{
	[SerializeField] protected Entity entity;

	public Entity GetEntity()
	{
		return entity;
	}

	public virtual void Possess(IPossessable previouslyPossessed)
	{
		entity.SetOutline();
	}

	public virtual void Unpossess(IPossessable newlyPossessed)
	{
		entity.ClearOutline();
	}
}