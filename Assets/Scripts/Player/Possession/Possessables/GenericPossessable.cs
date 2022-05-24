using UnityEngine;

public class GenericPossessable : MonoBehaviour, IPossessable {
	[SerializeField] private Entity entity;
	[SerializeField] private Rigidbody rb;

	public Entity GetEntity() {
		return entity;
	}
	public void Possess(IPossessable previouslyPossessed) {
		Rigidbody previouslyPossessedRB = previouslyPossessed.GetEntity().GetComponent<Rigidbody>();

		if (previouslyPossessedRB != null)
			rb.velocity = previouslyPossessedRB.velocity;

		foreach(var ren in entity.renderers)
		{
			ren.gameObject.layer = LayerMask.NameToLayer("Outline");
		}
	}

	public void Unpossess(IPossessable newlyPossessed) {
		rb.velocity = Vector3.zero;

		for (int i = 0; i < entity.renderers.Length; i++)
		{
			entity.renderers[i].gameObject.layer = entity.rendererLayers[i];
		}
	}
}
