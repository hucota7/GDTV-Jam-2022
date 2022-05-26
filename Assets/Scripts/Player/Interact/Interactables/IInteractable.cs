public interface IInteractable {
	Entity GetEntity();
	bool CanInteract(Entity interactor);
	void Interact(Entity interactor);
}
