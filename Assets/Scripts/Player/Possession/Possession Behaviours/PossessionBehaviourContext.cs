using System.Collections.Generic;
using UnityEngine;

public class PossessionBehaviourContext {
	public float possessionRadius;
	public List<IPossessable> possessables;
	public IPossessable currentPossessed;
	public Vector3 currentPossessedPosition;
}
