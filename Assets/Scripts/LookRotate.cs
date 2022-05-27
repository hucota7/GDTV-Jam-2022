using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookRotate : MonoBehaviour
{
	public Transform target;

	private void Update()
	{
		transform.rotation = Quaternion.LookRotation(target.position - transform.position, target.up);
	}
}
