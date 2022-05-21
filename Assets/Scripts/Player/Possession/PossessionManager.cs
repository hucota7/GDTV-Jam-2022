using System;
using System.Collections.Generic;
using UnityEngine;

public class PossessionManager : MonoBehaviour {
	public Action<IPossessable> Possessed;

	[SerializeField] private HumanPossessable startingPossable;
	[Space]
	[SerializeField] private LayerMask possessableMask;
	[SerializeField] private PossessionBehaviourConcreteImplementation possessionBehaviour;
	[Space]
	[SerializeField] private float possessionRadius;

	private IPossessable currentPossessed;

	private void Awake() {
		currentPossessed = startingPossable;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space))
			Possess();
	}

	private void Possess() {
		Vector3 currentPossessedPosition = currentPossessed.GetEntity().transform.position;

		var possessableObjects = Physics.OverlapSphere(currentPossessedPosition, possessionRadius, possessableMask, QueryTriggerInteraction.Collide);
		List<IPossessable> possessables = new List<IPossessable>(possessableObjects.Length);

		foreach (var possessableObject in possessableObjects) {
			possessables.Add(possessableObject.GetComponent<IPossessable>());
		}

		PossessionBehaviourContext context = new PossessionBehaviourContext() {
			currentPossessed = currentPossessed,
			possessables = possessables,
			possessionRadius = possessionRadius,
			currentPossessedPosition = currentPossessedPosition
		};

		IPossessable newPossessable = possessionBehaviour.GetPossessable(context);

		if (newPossessable == null)
			return;

		newPossessable.Possess(currentPossessed);
		currentPossessed.Unpossess(newPossessable);
		currentPossessed = newPossessable;

		Possessed?.Invoke(currentPossessed);
	}

	private void OnDrawGizmosSelected() {
		Gizmos.DrawWireSphere(currentPossessed.GetEntity().transform.position, possessionRadius);
	}
}
