using UnityEngine;

public class HumanPossessable : MonoBehaviour, IPossessable {
	[SerializeField] private Entity entity;
	[SerializeField] private Rigidbody rb;

	public Entity GetEntity() {
		return entity;
	}
	public void Possess(IPossessable previouslyPossessed) {
		Rigidbody previouslyPossessedRB = previouslyPossessed.GetEntity().GetComponent<Rigidbody>();

		if (previouslyPossessedRB != null)
			rb.velocity = previouslyPossessedRB.velocity;
	}

	public void Unpossess(IPossessable newlyPossessed) {
		rb.velocity = Vector3.zero;
	}
}
