using UnityEngine;

[CreateAssetMenu(fileName = "Closest Else Ghost", menuName = "Scriptable Object/Possession Behaviour/Closest Else Ghost")]
public class ClosestElseGhostPossessionBehaviour : PossessionBehaviourConcreteImplementation
{
	public GhostPossessable ghostPossessable;
	[Space]
	[SerializeField] private float possessionRadius;
	[SerializeField] private LayerMask possessableMask;

	public override IPossessable GetPossessable(PossessionBehaviourContext context)
	{
		Vector3 currentPossessedPosition = context.currentPossessed.GetEntity().transform.position;

		var possessableObjects = Physics.OverlapSphere(currentPossessedPosition, possessionRadius, possessableMask, QueryTriggerInteraction.Collide);
		IPossessable[] possessables = new IPossessable[possessableObjects.Length];

		for (int i = 0; i < possessableObjects.Length; i++)
		{
			possessables[i] = possessableObjects[i].GetComponent<IPossessable>();
		}

		IPossessable closestPossessable = null;
		float closestDistance = possessionRadius;

		foreach (var possessable in possessables)
		{
			if (possessable == null)
				continue;
			if (possessable == context.currentPossessed)
				continue;

			float distance = Vector3.Distance(currentPossessedPosition, possessable.GetEntity().transform.position);

			if (distance < closestDistance)
			{
				closestPossessable = possessable;
				closestDistance = distance;
			}
		}

		if (closestDistance == possessionRadius)
		{
			return (context.currentPossessed != (IPossessable)ghostPossessable) ? ghostPossessable : null;
		}
		else
		{
			return closestPossessable;
		}
	}
}