using UnityEngine;

[CreateAssetMenu(fileName = "Null Possessable", menuName = "Scriptable Object/Possessable/Null")]
public class NullPossessable : ScriptableObject, IPossessable {
	public Entity GetEntity() {
		return null;
	}

	public void Possess(IPossessable previouslyPossessed) {
	}

	public void Unpossess(IPossessable newlyPossessed) {
	}
}
