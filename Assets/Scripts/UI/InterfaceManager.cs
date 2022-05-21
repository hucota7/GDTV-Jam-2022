using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
	[Header("References")]
	public Canvas mainCanvas, worldCanvas;

	[Header("Prefabs")]
	public EnergyBarUI energyBarPrefab;
	public static InterfaceManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
