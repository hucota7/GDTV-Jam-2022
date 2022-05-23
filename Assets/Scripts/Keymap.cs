using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Keymap
{
	public static bool LukeMode = false;

	public static KeyCode Left	=> LukeMode ? KeyCode.S : KeyCode.A;
	public static KeyCode Right	=> LukeMode ? KeyCode.T : KeyCode.D;
	public static KeyCode Up	=> LukeMode ? KeyCode.R : KeyCode.W;
	public static KeyCode Down	=> LukeMode ? KeyCode.H : KeyCode.S;
	public static KeyCode Possess => LukeMode ? KeyCode.Space : KeyCode.Space;
	public static KeyCode Use	=> LukeMode ? KeyCode.A : KeyCode.E;
}
