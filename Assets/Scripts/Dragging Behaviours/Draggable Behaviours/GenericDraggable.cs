using UnityEngine;

public class GenericDraggable : MonoBehaviour, IDraggable {
	[SerializeField] private Transform dragLinkPoint;
	[SerializeField] private Collider dragLinkPointCollider;
	[SerializeField] private Entity entity;

	public void Drag(IDragging dragger) {
		dragLinkPointCollider.transform.position = dragger.GetDragLinkPoint().position;
	}

	public Transform GetDragLinkPoint() {
		return dragLinkPoint;
	}

    public Entity GetEntity()
    {
		return entity;
    }
}
