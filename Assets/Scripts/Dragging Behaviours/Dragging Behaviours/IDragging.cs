using UnityEngine;

public interface IDragging {
	void StartDragging(IDraggable dragged);
	void StopDragging(IDraggable dragged);
	Transform GetDragLinkPoint();
}
