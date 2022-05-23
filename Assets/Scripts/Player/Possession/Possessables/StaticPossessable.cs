using UnityEngine;

public class StaticPossessable : MonoBehaviour, IPossessable
{
	[SerializeField] private Entity entity;

	public Entity GetEntity()
	{
		return entity;
	}

	public void Possess(IPossessable previouslyPossessed)
	{
		Rigidbody previouslyPossessedRB = previouslyPossessed.GetEntity().GetComponent<Rigidbody>();

		foreach (var ren in entity.renderers)
		{
			ren.gameObject.layer = LayerMask.NameToLayer("Outline");
		}
	}

	public void Unpossess(IPossessable newlyPossessed)
	{
		foreach (var ren in entity.renderers)
		{
			ren.gameObject.layer = 0;
		}
	}
}
