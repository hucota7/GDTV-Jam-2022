using UnityEngine;

public class GenericDraggable : MonoBehaviour, IDraggable {
	[SerializeField] private Transform dragLinkPoint;
	[SerializeField] private Collider dragLinkPointCollider;
	[SerializeField] private Entity entity;
	[Space]
	[SerializeField] private float disconnectDistance;

	public void Drag(IDragging dragger) {
		Vector3 draggerLinkPointLateral = dragger.GetDragLinkPoint().position;
		draggerLinkPointLateral.y = 0;

		Vector3 entityLateralPosition = GetEntity().transform.position;
		entityLateralPosition.y = 0;

		if (Vector3.Distance(draggerLinkPointLateral, entityLateralPosition) > disconnectDistance) {
			dragger.StopDragging(this);
			return;
		}

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
