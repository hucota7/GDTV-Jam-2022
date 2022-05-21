using UnityEngine;

public abstract class PossessionBehaviourConcreteImplementation : ScriptableObject, IpossessionBehaviour {
	public abstract IPossessable GetPossessable(PossessionBehaviourContext context);
}
