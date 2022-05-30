using UnityEngine;

public class GhostPossessable : Possessable
{
	[SerializeField] private GhostMovement movement;
	[SerializeField] private GameObject visuals;

	public override void Possess(IPossessable previouslyPossessed)
	{
		base.Possess(previouslyPossessed);

		Outline.SetColor(new Color(1, 0.33f, 0));

		// yes, this *is* necessary.
		movement.CC.enabled = false;
		//transform.SetParent(transform.parent ? transform.parent.parent : null);
		transform.SetParent(null);
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

	public override void Unpossess(IPossessable newlyPossessed)
	{
		base.Unpossess(newlyPossessed);

		Outline.SetColor(Color.cyan);

		if (entity.EnergyBar) entity.EnergyBar.HideBar();
		visuals.SetActive(false);
		movement.CC.detectCollisions = false;

		movement.velocity = Vector3.zero;

		transform.SetParent(((Component)newlyPossessed).transform);
		transform.localPosition = Vector3.zero;
	}
}