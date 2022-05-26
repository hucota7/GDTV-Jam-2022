using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{   
    private void OnCollisionEnter(Collision collision)
    {
		if (collision.transform.GetComponentInParent<Entity>() is Entity e)
		{
			Debug.Log($"{e.gameObject.name} fell out of the world");
			Destroy(e.gameObject);
		}
    }
}
