using System;
using UnityEngine;

public class PossessionManager : MonoBehaviour {
	public Action<IPossessable> Possessed;

	[SerializeField] private HumanPossessable startingPossable;
	[Space]
	[SerializeField] private PossessionBehaviourConcreteImplementation possessionBehaviour;

	private IPossessable currentPossessed;

	private void Awake() {
		currentPossessed = startingPossable;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space))
			Possess();
	}

	private void Possess() {
		PossessionBehaviourContext context = new PossessionBehaviourContext() {
			currentPossessed = currentPossessed
		};

		IPossessable newPossessable = possessionBehaviour.GetPossessable(context);

		if (newPossessable == null)
			return;

		newPossessable.Possess(currentPossessed);
		currentPossessed.Unpossess(newPossessable);
		currentPossessed = newPossessable;

		Possessed?.Invoke(currentPossessed);
	}
}
