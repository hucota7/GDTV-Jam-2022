using UnityEngine;

public class InteractionManager : MonoBehaviour {
	[SerializeField] private PossessionManager possessionManager;
	[Space]
	[SerializeField] private GetInteractableBehaviourConcreteImplementation getInteractableBehaviour;

	private Entity currentInteractor;

	private void Awake() {
		possessionManager.InitialPossession += SetInteractor;
		possessionManager.Possessed += SetInteractor;
	}

	private void FixedUpdate() {
		if (Input.GetKeyDown(Keymap.Interact))
			Interact(currentInteractor);
	}

	public void SetInteractor(IPossessable interactor) => currentInteractor = interactor.GetEntity();

	public void Interact(Entity interactor) {
		IInteractable interactable = getInteractableBehaviour.GetInteractable(interactor);

		if (interactable == null)
			return;

		interactable.TryInteract(interactor);
	}
}
