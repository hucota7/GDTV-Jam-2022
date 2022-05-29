using UnityEngine;

public class UseAbilityInteractable : MonoBehaviour, IInteractable {
	[SerializeField] private Entity entity;

	private IUseable useable;

	private void Awake() {
		//Cannot serialise interfaces so must be found at awake instead
		useable = entity.GetComponent<IUseable>();
	}

	public bool CanInteract(Entity interactor) {
		return !(interactor is GhostEntity);
	}

	public Entity GetEntity() {
		return entity;
	}

	public void Interact(Entity interactor) {
		useable.Use();
	}
}
