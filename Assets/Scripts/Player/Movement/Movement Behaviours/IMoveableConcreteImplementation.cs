using UnityEngine;

public abstract class IMoveableConcreteImplementation : MonoBehaviour, IMoveable {
	public abstract void Move(Vector3 direction);
}
