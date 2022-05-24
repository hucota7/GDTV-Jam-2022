using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PatrolInstruction : MonoBehaviour
{
	public abstract IEnumerator Process(Character character);
}
