using UnityEngine;

public class MovementManager : MonoBehaviour {
	[SerializeField] private PossessionManager possessionManager;
	[Space]
	[SerializeField] private GameObject protag;

	private IMoveable currentPossessedMovable;

	private void Awake() {
		possessionManager.Possessed += SetCurrentPossessedMovable;
		currentPossessedMovable = protag.GetComponent<IMoveable>();
	}

	private void FixedUpdate() {
		if (currentPossessedMovable != null)
		{
			Vector3 inputDirection = GetInputDirection();

			currentPossessedMovable.Move(inputDirection);
		}
	}

	private void SetCurrentPossessedMovable(IPossessable newPossessable) {
		SetCurrentPossessedMovable(newPossessable.GetEntity().GetComponent<IMoveable>());
	}

	public void SetCurrentPossessedMovable(IMoveable newPossessionMovable) {
		currentPossessedMovable = newPossessionMovable;
	}

	private Vector3 GetInputDirection() {
		Vector3 direction = Vector3.zero;

		if (Input.GetKey(Keymap.Left))
			direction.x--;

		if (Input.GetKey(Keymap.Right))
			direction.x++;

		if (Input.GetKey(Keymap.Up))
			direction.z++;

		if (Input.GetKey(Keymap.Down))
			direction.z--;

		return direction.normalized;
	}
}
