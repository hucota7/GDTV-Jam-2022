using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEntity : Entity {
	[SerializeField] public PossessionManager possessionManager;
	[SerializeField] public GhostPossessable ghostPossessable;
	[SerializeField] private GameObject visuals;

	public override void Awake() {
		base.Awake();
		possessionManager.InitialPossession += InitialPossesion;
	}

	public override void Start() {
		base.Start();
		if (EnergyBar) EnergyBar.HideBar();
	}

	private void InitialPossesion(IPossessable possessable) {
		visuals.SetActive(possessable == (IPossessable)ghostPossessable);
	}
}