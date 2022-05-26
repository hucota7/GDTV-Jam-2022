using UnityEngine;

public interface IDraggable {
	Transform GetDragLinkPoint();
	void Drag(IDragging dragger);

	Entity GetEntity();
}
