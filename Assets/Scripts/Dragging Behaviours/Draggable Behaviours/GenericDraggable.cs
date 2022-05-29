using UnityEngine;

public class GenericDraggable : MonoBehaviour, IDraggable {
	[SerializeField] private Transform dragLinkPoint;
	[SerializeField] private Collider dragLinkPointCollider;
	[SerializeField] private GameObject colliderRoot;
	[SerializeField] private Entity entity;

	private Vector3 previousDraggerLinkPointPosition;

	public void Drag(IDragging dragger) {
		if (previousDraggerLinkPointPosition == Vector3.zero)
			previousDraggerLinkPointPosition = dragger.GetDragLinkPoint().position;

		Vector3 movement = dragger.GetDragLinkPoint().position - previousDraggerLinkPointPosition;
		movement.y = 0;

		GetEntity().transform.position += movement;
		colliderRoot.transform.position -= movement;

		dragLinkPointCollider.transform.position = dragger.GetDragLinkPoint().position;

		previousDraggerLinkPointPosition = dragger.GetDragLinkPoint().position;
	}

	public Transform GetDragLinkPoint() {
		return dragLinkPoint;
	}

    public Entity GetEntity()
    {
		return entity;
    }
}
