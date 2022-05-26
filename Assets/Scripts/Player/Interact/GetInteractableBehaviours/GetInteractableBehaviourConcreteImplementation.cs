using UnityEngine;

public abstract class GetInteractableBehaviourConcreteImplementation : ScriptableObject, IGetInteractableBehaviour {
	public abstract IInteractable GetInteractable(Entity interactor);
}
