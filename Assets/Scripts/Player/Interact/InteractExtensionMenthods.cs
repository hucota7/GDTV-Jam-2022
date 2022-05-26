public static class InteractExtensionMenthods {
	public static void TryInteract(this IInteractable interactable, Entity interactor) {
		if (interactable.CanInteract(interactor))
			interactable.Interact(interactor);
	}
}
