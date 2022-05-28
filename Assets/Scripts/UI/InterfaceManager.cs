using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
	[Header("References")]
	public Canvas mainCanvas;
	public Canvas worldCanvas;

	public UIScreen gameOverScreen, creditsScreen;

	[Header("Prefabs")]
	public EnergyBarUI energyBarPrefab;
	public ThoughtBubble thoughtBubblePrefab;
	public KeyPrompt keyPromptPrefab;
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

	public IEnumerator DoFade(UIScreen screen, float speed = 1f)
    {
		screen.Group.alpha = 0;
		while (screen.Group.alpha < 1)
		{
			screen.Group.alpha += Time.deltaTime * speed;
			yield return null;
		}
		screen.Group.alpha = 1;
	}

	public void DisplayGameOver()
    {
		UIScreen.Focus(gameOverScreen);
		StartCoroutine(DoFade(gameOverScreen, 1));
    }
	public void DisplayCreditsScreen()
	{
		UIScreen.Focus(creditsScreen);
		StartCoroutine(DoFade(creditsScreen, 1));
	}
}
