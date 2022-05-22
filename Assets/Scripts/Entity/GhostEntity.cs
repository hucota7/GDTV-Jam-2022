using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEntity : Entity {
	[SerializeField] public PossessionManager possessionManager;
	[SerializeField] public GhostPossessable ghostPossessable;
	[SerializeField] private GameObject visuals;

	private void Awake() {
		possessionManager.InitialPossession += InitialPossesion;
	}

	public override void Start() {
		base.Start();
		base.energyBar.HideBar();
	}

	private void InitialPossesion(IPossessable possessable) {
		if (possessable != (IPossessable)ghostPossessable)
			return;

		visuals.SetActive(true);
	}
}