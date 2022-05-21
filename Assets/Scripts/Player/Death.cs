using System;
using UnityEngine;

public class Death : MonoBehaviour {
	public Action PlayerDies;
	public Action PlayerRevives;

	private bool dead;

	private void Update() {
		if (Input.GetKey(KeyCode.Space))
			ToggleDeath();
	}

	private void ToggleDeath() {
		dead = !dead;

		if (dead) {
			PlayerDies?.Invoke();
		}
		else
			PlayerRevives?.Invoke();
	}
}
