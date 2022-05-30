using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : Door
{
	public TrapDoorHole hole;

	public override void Reset()
	{
		base.Reset();
		hole = GetComponentInChildren<TrapDoorHole>(true);
	}

	public override void OpenDoors()
	{
		base.OpenDoors();

		if (!hole.gameObject.activeSelf)
			hole.gameObject.SetActive(true);
	}

	public override void CloseDoors()
	{
		base.CloseDoors();

		if (hole.gameObject.activeSelf)
			hole.gameObject.SetActive(false);
	}
}
