using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{   
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Destroyed something because it fell through floor");
        Destroy(collision.gameObject);
    }
}
