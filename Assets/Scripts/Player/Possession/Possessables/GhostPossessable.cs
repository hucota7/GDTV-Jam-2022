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
		if (entity.energyBar) entity.energyBar.ShowBar();
		visuals.SetActive(true);

		if (previouslyPossessed?.GetEntity() is Entity e)
		{
			Rigidbody previouslyPossessedRB = e.GetComponent<Rigidbody>();

			if (previouslyPossessedRB != null)
				rb.velocity = previouslyPossessedRB.velocity;

			transform.position = e.transform.position;
		}
	}

	public void Unpossess(IPossessable newlyPossessed) {
		entity.energyBar.HideBar();
		visuals.SetActive(false);

		rb.velocity = Vector3.zero;

		transform.SetParent(((Component)newlyPossessed).transform);
	}
}
