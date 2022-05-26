using UnityEngine;

[CreateAssetMenu(fileName = "Closest GetInteractable Behaviour", menuName = "Scriptable Object/GetInteractable Behaviour/Closest")]
public class ClosestGetInteractableBehaviour : GetInteractableBehaviourConcreteImplementation {
	[SerializeField] private float interactRadius;
	[SerializeField] private LayerMask interactableMask;

	public override IInteractable GetInteractable(Entity interactor) {
		Vector3 interactorPosition = interactor.transform.position;

		var possessableObjects = Physics.OverlapSphere(interactorPosition, interactRadius, interactableMask, QueryTriggerInteraction.Collide);
		IInteractable[] interactables = new IInteractable[possessableObjects.Length];

		for (int i = 0; i < possessableObjects.Length; i++) {
			interactables[i] = possessableObjects[i].GetComponent<IInteractable>();
		}

		IInteractable closestInteractable = null;
		float closestDistance = interactRadius;

		foreach (var interactable in interactables) {
			if (interactable == null)
				continue;

			if (interactable.GetEntity() == interactor)
				continue;

			float distance = Vector3.Distance(interactorPosition, interactable.GetEntity().transform.position);

			if (distance < closestDistance) {
				closestInteractable = interactable;
				closestDistance = distance;
			}
		}

		return closestInteractable;
	}
}
