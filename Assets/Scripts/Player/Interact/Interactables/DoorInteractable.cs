using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable {
	[SerializeField] private Door doorEntity;

	private IUseable useable;

	private void Awake() {
		//Cannot serialise interfaces so must be found at awake instead
		useable = doorEntity.GetComponent<IUseable>();
	}

	public bool CanInteract(Entity interactor) {
		bool holdingKey = interactor is Character && (interactor as Character).HoldingKey;

		return !doorEntity.requiresKey || holdingKey;
	}

	public Entity GetEntity() {
		return doorEntity;
	}

	public void Interact(Entity interactor) {
		doorEntity.requiresKey = false;
		useable.Use();

		if (interactor is Character)
			(interactor as Character).RemoveKey();
	}
}
