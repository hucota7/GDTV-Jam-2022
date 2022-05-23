using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
	[field: SerializeField] public float MaxValue { get; private set; } = 100;
	[field: SerializeField, ReadOnly] public float Value { get; private set; } = 100;

	private void Awake()
	{
		Value = MaxValue;
	}

	public bool Use(float amount)
	{
		Value = Value - amount;
		if (Value < 0) Value = 0;
		return Value > 0;
	}

	private void OnValidate()
	{
		Value = MaxValue;
	}
}
