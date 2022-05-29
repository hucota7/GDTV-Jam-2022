using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) {
		Debug.Log(other.gameObject);
		if (other.transform.GetComponentInParent<Entity>() is Entity e)
		{
			Debug.Log($"{e.gameObject.name} fell out of the world");

			IPossessable possessable = e.gameObject.GetComponent<IPossessable>();

			if (possessable is Possessable)
				(possessable as Possessable).MarkForDestruction();

			if (possessable != null && PossessionManager.Instance.CurrentPossessed == possessable)
				PossessionManager.Instance.Possess();

			Destroy(e.gameObject); //find fix that also destroys rats
		}
    }
}
