using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponentInParent<Entity>(true) is Entity e && e.CompareTag("Player"))
		{
			GameManager.Goal();
		}
	}
}
