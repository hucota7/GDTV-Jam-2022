using UnityEngine;

public class Possessable : MonoBehaviour, IPossessable
{
	[SerializeField] protected Entity entity;

	public bool IsPossessed { get; private set; }

	public Entity GetEntity()
	{
		return entity;
	}

	public virtual void Possess(IPossessable previouslyPossessed)
	{
		IsPossessed = true;
		entity.SetOutline();
		//AudioManager.Play("PossessionSFX");
		//except when "possessing" the ghost
	}

	public virtual void Unpossess(IPossessable newlyPossessed)
	{
		IsPossessed = false;
		entity.ClearOutline();
		//AudioManager.Play("UnpossessionSFX");
		//except when "unpossessing" the ghost
	}
}