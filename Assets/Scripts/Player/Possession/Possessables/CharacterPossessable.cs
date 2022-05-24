using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPossessable : MonoBehaviour, IPossessable
{
	public Character characterEntity;

	public Entity GetEntity() => characterEntity;

	public void Possess(IPossessable previouslyPossessed)
	{
		Rigidbody previouslyPossessedRB = previouslyPossessed.GetEntity().GetComponent<Rigidbody>();

		if (previouslyPossessedRB != null)
			characterEntity.velocity = previouslyPossessedRB.velocity;

		foreach (var ren in characterEntity.renderers)
		{
			ren.gameObject.layer = LayerMask.NameToLayer("Outline");
		}
	}

	public void Unpossess(IPossessable newlyPossessed)
	{
		characterEntity.Stop();

		for (int i = 0; i < characterEntity.renderers.Length; i++)
		{
			characterEntity.renderers[i].gameObject.layer = characterEntity.rendererLayers[i];
		}
	}
}
