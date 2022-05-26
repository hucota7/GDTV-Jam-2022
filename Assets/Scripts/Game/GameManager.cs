using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public Level[] levels;

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

    public static void Goal()
	{
		// if not last level then go to next else show end screen
		Debug.Log("Goal!");
	}

	public static void GameOver()
    {
		InterfaceManager.Instance.DisplayGameOver();
    }
}
