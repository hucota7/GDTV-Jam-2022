using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanPathfind : IMoveable
{
	bool TryPathfind(Vector3 worldPosition);
}
