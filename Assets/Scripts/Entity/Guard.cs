using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Character
{
	[field: SerializeField] public PatrolRoute Route { get; private set; }
	Possessable guardPossessable;

	bool wasPossessed = false;

	public override void Awake()
	{
		base.Awake();
		guardPossessable = GetComponent<Possessable>();
	}

	public override void Start()
    {
        base.Start();
		StartCoroutine(FollowRoute());
	}

	public override void Update()
	{
		base.Update();
		
		if (wasPossessed && !guardPossessable.IsPossessed)
		{
			Debug.Log("Starting route");
			StartCoroutine(FollowRoute());
		}
		else if (!wasPossessed && guardPossessable.IsPossessed)
		{
			Debug.Log("Stopping route");
			StopAllCoroutines();
			agent.ResetPath();
		}

		wasPossessed = guardPossessable.IsPossessed;
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
