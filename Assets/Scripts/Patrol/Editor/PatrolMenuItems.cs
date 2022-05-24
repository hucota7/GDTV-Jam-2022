using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PatrolInstructions;

public class PatrolMenuItems
{
	[MenuItem("GameObject/Create Patrol Route", true)]
	static bool CreateRoute_Validate()
	{
		if (Selection.activeGameObject is GameObject go)
		{
			if (go.TryGetComponent(out PatrolRoute _)
				|| go.TryGetComponent(out PatrolNode _)
				|| go.TryGetComponent(out PatrolInstruction _))
				return false;
		}
		return true;
	}

	[MenuItem("GameObject/Create Patrol Route", priority = 0)]
	static void CreateRoute(MenuCommand command)
	{
		GameObject go = new GameObject("Patrol Route", typeof(PatrolRoute));
		GameObjectUtility.SetParentAndAlign(go, command.context as GameObject);
		Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
		Selection.activeObject = go;
	}

	[MenuItem("GameObject/Create Patrol Node", true)]
	static bool CreateNode_Validate()
	{
		return Selection.activeGameObject is GameObject go && go.TryGetComponent(out PatrolRoute _);
	}

	[MenuItem("GameObject/Create Patrol Node", priority = 0)]
	static void CreateNode(MenuCommand command)
	{
		GameObject go = new GameObject("Patrol Node", typeof(PatrolNode));
		GameObjectUtility.SetParentAndAlign(go, command.context as GameObject);
		Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
		Selection.activeObject = go;
	}

	[MenuItem("GameObject/Create Patrol Instruction/Wait", true)]
	[MenuItem("GameObject/Create Patrol Instruction/Look", true)]
	static bool CreateInstruction_Validate()
	{
		return Selection.activeGameObject is GameObject go && go.TryGetComponent(out PatrolNode _);
	}

	[MenuItem("GameObject/Create Patrol Instruction/Wait", priority = 0)]
	static void CreateInstructionWait(MenuCommand command)
	{
		GameObject go = new GameObject("Wait Instruction", typeof(WaitInstruction));
		GameObjectUtility.SetParentAndAlign(go, command.context as GameObject);
		Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
		Selection.activeObject = go;
	}

	[MenuItem("GameObject/Create Patrol Instruction/Look", priority = 0)]
	static void CreateInstructionLook(MenuCommand command)
	{
		GameObject go = new GameObject("Look Instruction", typeof(LookInstruction));
		GameObjectUtility.SetParentAndAlign(go, command.context as GameObject);
		Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
		Selection.activeObject = go;
	}
}
