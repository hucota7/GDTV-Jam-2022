using UnityEngine;

public class MovementManager : MonoBehaviour
{
	private IMoveable currentPossessedMovable;

	private void Awake()
	{
		PossessionManager.Instance.InitialPossession += SetCurrentPossessedMovable;
		PossessionManager.Instance.Possessed += SetCurrentPossessedMovable;
	}


	private void Update()
	{
		if (currentPossessedMovable is IUseable useable)
		{
			if (Input.GetKeyDown(Keymap.Use)) useable.Use();
		}
	}

	private void FixedUpdate()
	{
		if (currentPossessedMovable != null)
		{
			Vector3 inputDirection = GetInputDirection();

			currentPossessedMovable.Move(inputDirection);
		}
	}

	private void SetCurrentPossessedMovable(IPossessable newPossessable)
	{
		SetCurrentPossessedMovable(newPossessable.GetEntity().GetComponent<IMoveable>());
	}

	public void SetCurrentPossessedMovable(IMoveable newPossessionMovable)
	{
		currentPossessedMovable = newPossessionMovable;
	}

	private Vector3 GetInputDirection()
	{
		Vector3 direction = Vector3.zero;

		if (Input.GetKey(Keymap.Left))
			direction.x--;

		if (Input.GetKey(Keymap.Right))
			direction.x++;

		if (Input.GetKey(Keymap.Up))
			direction.z++;

		if (Input.GetKey(Keymap.Down))
			direction.z--;

		float cameraRotation = (-Camera.main.transform.localEulerAngles.y) * Mathf.Deg2Rad;

		Vector3 cameraRelativeDirection = direction;
		cameraRelativeDirection.x = direction.x * Mathf.Cos(cameraRotation) - direction.z * Mathf.Sin(cameraRotation);
		cameraRelativeDirection.z = direction.x * Mathf.Sin(cameraRotation) + direction.z * Mathf.Cos(cameraRotation);
		
		return cameraRelativeDirection.normalized;
	}
}