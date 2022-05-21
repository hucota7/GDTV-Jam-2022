using UnityEngine;

public class ToggleGhostSceneProxy : MonoBehaviour {
	[SerializeField] private ToggleGhostPossessionBehaviour toggleGhostPossessionBehaviour;
	[SerializeField] private GhostPossessable ghostPossessable;

	private void Awake() {
		toggleGhostPossessionBehaviour.ghostPossessable = ghostPossessable;
	}
}
