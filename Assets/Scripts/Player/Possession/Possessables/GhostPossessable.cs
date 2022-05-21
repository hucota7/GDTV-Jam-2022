using UnityEngine;

public class GhostPossessable : MonoBehaviour, IPossessable {
	[SerializeField] private Entity entity;
	[SerializeField] private Rigidbody rb;
	[SerializeField] private GameObject visuals;

	public Entity GetEntity() {
		return entity;
	}

	public void Possess(IPossessable previouslyPossessed) {
		transform.SetParent(transform.parent ? transform.parent.parent : null);
		entity.energyBar.ShowBar();
		visuals.SetActive(true);

		Rigidbody previouslyPossessedRB = previouslyPossessed.GetEntity().GetComponent<Rigidbody>();

		if (previouslyPossessedRB != null)
			rb.velocity = previouslyPossessedRB.velocity;

		transform.position = previouslyPossessed.GetEntity().transform.position;
	}

	public void Unpossess(IPossessable newlyPossessed) {
		entity.energyBar.HideBar();
		visuals.SetActive(false);

		rb.velocity = Vector3.zero;

		transform.SetParent(((Component)newlyPossessed).transform);
	}
}
