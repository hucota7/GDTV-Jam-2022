using UnityEngine;

public class InteractionManager : MonoBehaviour
{
	[SerializeField] private GetInteractableBehaviourConcreteImplementation getInteractableBehaviour;

	private Entity currentInteractor;

	private void Awake()
	{
		PossessionManager.Instance.InitialPossession += SetInteractor;
		PossessionManager.Instance.Possessed += SetInteractor;
	}

	private void FixedUpdate()
	{
		if (Input.GetKeyDown(Keymap.Interact))
			Interact(currentInteractor);
	}

	public void SetInteractor(IPossessable interactor) => currentInteractor = interactor.GetEntity();

	public void Interact(Entity interactor)
	{
		IInteractable interactable = getInteractableBehaviour.GetInteractable(interactor);

		if (interactable == null)
			return;

		interactable.TryInteract(interactor);
	}
}