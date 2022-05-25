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
        //AudioManager.Play("PossessionSFX");
		//except when "possessing" the ghost
    }

    public virtual void Unpossess(IPossessable newlyPossessed)
	{
		entity.ClearOutline();
		//AudioManager.Play("UnpossessionSFX");
		//except when "unpossessing" the ghost
	}
}