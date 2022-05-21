using UnityEngine;

[CreateAssetMenu(fileName = "Closest Else Ghost", menuName = "Scriptable Object/Possession Behaviour/Closest Else Ghost")]
public class ClosestElseGhostPossessionBehaviour : PossessionBehaviourConcreteImplementation {
	public GhostPossessable ghostPossessable;

	public override IPossessable GetPossessable(PossessionBehaviourContext context) {
		IPossessable closestPossesable = null;
		float closestDistance = context.possessionRadius;

		foreach (var possessable in context.possessables) {
			if (possessable == context.currentPossessed)
				continue;

			float distance = Vector3.Distance(context.currentPossessedPosition, possessable.GetEntity().transform.position);

			if (distance < closestDistance) {
				closestPossesable = possessable;
				closestDistance = distance;
			}
		}

		if (closestDistance == context.possessionRadius) {
			return (context.currentPossessed != ghostPossessable) ? ghostPossessable : null;
		}
		else {
			return closestPossesable;
		}
	}
}
