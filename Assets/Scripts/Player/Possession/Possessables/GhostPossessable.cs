using UnityEngine;

public class GhostPossessable : MonoBehaviour, IPossessable {
	[SerializeField] private Entity entity;
	[SerializeField] private Rigidbody rb;
	[SerializeField] private MeshRenderer meshRenderer;

	public Entity GetEntity() {
		return entity;
	}

	public void Possess(IPossessable previouslyPossessed) {
		meshRenderer.enabled = true;

		Rigidbody previouslyPossessedRB = previouslyPossessed.GetEntity().GetComponent<Rigidbody>();

		if (previouslyPossessedRB != null)
			rb.velocity = previouslyPossessedRB.velocity;

		transform.position = previouslyPossessed.GetEntity().transform.position;
	}

	public void Unpossess(IPossessable newlyPossessed) {
		meshRenderer.enabled = false;
		rb.velocity = Vector3.zero;

		transform.position = newlyPossessed.GetEntity().transform.position;
	}
}
