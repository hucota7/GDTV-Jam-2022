using UnityEngine;

[CreateAssetMenu(fileName = "Toggle Ghost", menuName = "Scriptable Object/Possession Behaviour/Toggle Ghost")]
public class ToggleGhostPossessionBehaviour : PossessionBehaviourConcreteImplementation {
	public GhostPossessable ghostPossessable;
	[Space]
	[SerializeField] private float possessionRadius;
	[SerializeField] private LayerMask possessableMask;

	public override IPossessable GetPossessable(PossessionBehaviourContext context) {
		if (context.currentPossessed != (IPossessable)ghostPossessable)
			return ghostPossessable;

		Vector3 currentPossessedPosition = context.currentPossessed.GetEntity().transform.position;

		var possessableObjects = Physics.OverlapSphere(currentPossessedPosition, possessionRadius, possessableMask, QueryTriggerInteraction.Collide);
		IPossessable[] possessables = new IPossessable[possessableObjects.Length];

		for (int i = 0; i < possessableObjects.Length; i++) {
			possessables[i] = possessableObjects[i].GetComponent<IPossessable>();
			if (possessables[i] == null)
			{
				Debug.LogWarning($"{possessableObjects[i]} does not have an IPossessable component");
			}
		}

		IPossessable closestPossesable = null;
		float closestDistance = possessionRadius;

		foreach (var possessable in possessables) {
			if (possessable == null)
				continue;
			if (possessable == context.currentPossessed)
				continue;

			if (possessable.GetEntity() is Entity e)
			{
				float distance = Vector3.Distance(currentPossessedPosition, e.transform.position);

				if (distance < closestDistance)
				{
					closestPossesable = possessable;
					closestDistance = distance;
				}
			}
			else
			{
				Debug.LogWarning($"{possessable} doesn't have an entity");
			}
		}

		return (closestDistance != possessionRadius) ? closestPossesable : null;
	}
}
