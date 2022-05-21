using UnityEngine;

public class ClosestElseGhostSceneProxy : MonoBehaviour {
	[SerializeField] private ClosestElseGhostPossessionBehaviour closestElseGhostPossessionBehaviour;
	[SerializeField] private GhostPossessable ghostPossessable;

	private void Awake() {
		closestElseGhostPossessionBehaviour.ghostPossessable = ghostPossessable;
	}
}
