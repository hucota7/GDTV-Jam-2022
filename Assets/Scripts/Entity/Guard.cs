using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Character
{
	[field: SerializeField] public PatrolRoute Route { get; private set; }

    public override void Start()
    {
        base.Start();
       // GetComponent<Ragdoll>().ActivateRagdoll();
    }
}
