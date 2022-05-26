using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEntity : Entity {
	[SerializeField] public GhostPossessable ghostPossessable;
	[SerializeField] private GameObject visuals;

	public override void Awake() {
		base.Awake();
		PossessionManager.Instance.InitialPossession += InitialPossesion;
	}

	public override void Start() {
		base.Start();
		if (EnergyBar) EnergyBar.HideBar();
	}

	private void InitialPossesion(IPossessable possessable) {
		visuals.SetActive(possessable == (IPossessable)ghostPossessable);
	}
}