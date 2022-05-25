using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPossessable : Possessable
{
	Character characterEntity => entity as Character;

	public override void Possess(IPossessable previouslyPossessed)
	{
		base.Possess(previouslyPossessed);

		Rigidbody previouslyPossessedRB = previouslyPossessed.GetEntity().GetComponent<Rigidbody>();

		if (previouslyPossessedRB != null)
			characterEntity.velocity = previouslyPossessedRB.velocity;
		
	}

	public override void Unpossess(IPossessable newlyPossessed)
	{
		base.Unpossess(newlyPossessed);

		characterEntity.Stop();
	}

	private void OnValidate()
	{
		if (!(entity is Character))
			entity = null;
	}
}
