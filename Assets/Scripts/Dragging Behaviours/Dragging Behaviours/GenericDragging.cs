using UnityEngine;

public class GenericDragging : MonoBehaviour, IDragging {
	[SerializeField] private IMoveableConcreteImplementation currentMovement;
	[SerializeField] private IMoveableConcreteImplementation dragMovement;

	private GameObject movementObject;

	private void Awake() {
		movementObject = currentMovement.gameObject;
	}

	public Transform GetDragLinkPoint() {
		throw new System.NotImplementedException();
	}

	public void StartDragging(IDraggable dragged) {
		Destroy(movementObject.GetComponent(currentMovement.GetType()));
		movementObject.AddComponent(dragMovement.GetType());
	}

	public void StopDragging(IDraggable dragged) {
		Destroy(movementObject.GetComponent(dragMovement.GetType()));
		movementObject.AddComponent(currentMovement.GetType());
	}
}
