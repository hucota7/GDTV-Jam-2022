using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEntity : Entity {
	public override void Start() {
		base.Start();
		base.energyBar.HideBar();
	}
}