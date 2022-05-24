using UnityEngine;

public class GenericPossessable : MonoBehaviour, IPossessable {
	[SerializeField] private Entity entity;

	public Entity GetEntity() {
		return entity;
	}
	public void Possess(IPossessable previouslyPossessed) {
		foreach(var ren in entity.renderers)
		{
			ren.gameObject.layer = LayerMask.NameToLayer("Outline");
		}
	}

	public void Unpossess(IPossessable newlyPossessed) {
		for (int i = 0; i < entity.renderers.Length; i++)
		{
			entity.renderers[i].gameObject.layer = entity.rendererLayers[i];
		}
	}
}
