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
		}

		IPossessable closestPossesable = null;
		float closestDistance = possessionRadius;

		foreach (var possessable in possessables) {
			if (possessable == context.currentPossessed)
				continue;

			float distance = Vector3.Distance(currentPossessedPosition, possessable.GetEntity().transform.position);

			if (distance < closestDistance) {
				closestPossesable = possessable;
				closestDistance = distance;
			}
		}

		return (closestDistance != possessionRadius) ? closestPossesable : null;
	}
}
