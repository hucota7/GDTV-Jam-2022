using UnityEngine;

public class InteractionManager : MonoBehaviour
{
	[SerializeField] private GetInteractableBehaviourConcreteImplementation getInteractableBehaviour;

	private Entity currentInteractor;
	private IAdvancedInteractable previousAdvancedInteractable;

	private void Awake()
	{
		PossessionManager.Instance.InitialPossession += SetInteractor;
		PossessionManager.Instance.Possessed += SetInteractor;
	}

	private void Update()
	{
		if (Input.GetKeyDown(Keymap.Interact))
			Interact(currentInteractor);

		if (previousAdvancedInteractable == null)
			return;

		if (Input.GetKey(Keymap.Interact))
			InteractionHeld(currentInteractor);

		if (Input.GetKeyUp(Keymap.Interact))
			InteractionReleased(currentInteractor);
	}

	public void SetInteractor(IPossessable interactor) => currentInteractor = interactor.GetEntity();

	public void Interact(Entity interactor)
	{
		IInteractable interactable = getInteractableBehaviour.GetInteractable(interactor);

		if (interactable == null)
			return;

		interactable.TryInteract(interactor);

		previousAdvancedInteractable = (interactable is IAdvancedInteractable) ? (IAdvancedInteractable)interactable : null;
	}

	private void InteractionHeld(Entity interactor) {
		previousAdvancedInteractable.InteractionHeld(interactor);
	}

	private void InteractionReleased(Entity interactor) {
		previousAdvancedInteractable.InteractionRealeased(interactor);
	}
}