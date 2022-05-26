using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{   
    private void OnCollisionEnter(Collision collision)
    {
		Debug.Log(collision.gameObject);
		if (collision.transform.GetComponentInParent<Entity>() is Entity e)
		{
			Debug.Log($"{e.gameObject.name} fell out of the world");
			Destroy(e.gameObject); //find fix that also destroys rats
		}
    }
}
