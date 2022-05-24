using UnityEngine;

public class GhostPossessable : MonoBehaviour, IPossessable
{
	[SerializeField] private Entity entity;
	[SerializeField] private GhostMovement movement;
	[SerializeField] private GameObject visuals;

	public Entity GetEntity()
	{
		return entity;
	}

	public void Possess(IPossessable previouslyPossessed)
	{
		// yes, this *is* necessary.
		movement.CC.enabled = false;
		transform.SetParent(transform.parent ? transform.parent.parent : null);
		transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
		movement.CC.enabled = true;
		movement.CC.detectCollisions = true;

		if (entity.EnergyBar) entity.EnergyBar.ShowBar();
		visuals.SetActive(true);

		if (previouslyPossessed?.GetEntity() is Entity e)
		{
			Rigidbody previouslyPossessedRB = e.GetComponent<Rigidbody>();

			if (previouslyPossessedRB != null)
				movement.velocity = previouslyPossessedRB.velocity;

			transform.position = e.transform.position;
		}
	}

	public void Unpossess(IPossessable newlyPossessed)
	{
		if (entity.EnergyBar) entity.EnergyBar.HideBar();
		visuals.SetActive(false);
		movement.CC.detectCollisions = false;

		movement.velocity = Vector3.zero;

		transform.SetParent(((Component)newlyPossessed).transform);
		transform.localPosition = Vector3.zero;
	}
}