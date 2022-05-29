using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner:Character
{
	[field: SerializeField] public PatrolRoute Route { get; private set; }
	//Possessable possessable;

	bool wasPossessed = false;

	public override void Awake()
	{
		base.Awake();
		//possessable = GetComponent<Possessable>();
	}

	public override void Start()
	{
		base.Start();
		//GetComponent<Ragdoll>().ActivateRagdoll();
		StartCoroutine(FollowRoute());
	}

	public override void Update()
	{
		base.Update();

		//if (wasPossessed && !possessable.IsPossessed)
		//{
		//	Debug.Log("Starting route");
		//	StartCoroutine(FollowRoute());
		//}
		//else if (!wasPossessed && possessable.IsPossessed)
		//{
		//	Debug.Log("Stopping route");
		//	StopAllCoroutines();
		//}

		//wasPossessed = possessable.IsPossessed;
	}

	IEnumerator FollowRoute()
	{
		if (Route == null) yield break;

		IEnumerator route = Route.Process(this);
		while (Route)
		{
			yield return route;
		}
		Debug.Log(Route ? "End of route" : "Lost route");
	}
}
