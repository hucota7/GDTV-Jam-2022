using UnityEngine;

public class DragInteractable : MonoBehaviour, IAdvancedInteractable {
	[SerializeField] private Entity entity;

	public bool CanInteract(Entity interactor) {
		return interactor.GetComponent<IDragging>() != null;
	}

	public Entity GetEntity() {
		return entity;
	}

	public void Interact(Entity interactor) {
		interactor.GetComponent<IDragging>().StartDragging(entity.GetComponent<IDraggable>());
	}

	public void InteractionHeld(Entity interactor) { }

	public void InteractionRealeased(Entity interactor) {
		interactor.GetComponent<IDragging>().StopDragging(entity.GetComponent<IDraggable>());
	}
}
