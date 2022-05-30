using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
	[Header("References")]
	public Canvas mainCanvas;
	public Canvas worldCanvas;

	public UIScreen gameOverScreen, creditsScreen;
	public TMP_Text levelText;

	[Header("Prefabs")]
	public EnergyBarUI energyBarPrefab;
	public ThoughtBubble thoughtBubblePrefab;
	public KeyPrompt keyPromptPrefab;
	public GameObject playerCollectionPrefab;
	public static InterfaceManager Instance { get; private set; }

	public CanvasGroup fadeCG;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void SetLevelText(int levelIndex)
    {
		levelText.text = $"Level: {levelIndex}";
    }

	public void LevelTransitionFade(float speed)
    {
		StartCoroutine(LevelFade(speed));
    }

	public void EndGameFade(float speed)
    {
		StartCoroutine(EndGameFadeRoutine(speed));

	}

	public IEnumerator EndGameFadeRoutine(float speed)
    {
		fadeCG.alpha = 0;
		while (fadeCG.alpha < 1)
		{
			fadeCG.alpha += Time.deltaTime * speed;
			yield return null;
		}
		DisplayCreditsScreen();
	}

	public IEnumerator LevelFade(float speed = 1f)
	{		
		fadeCG.alpha = 0;
		while (fadeCG.alpha < 1)
		{
			fadeCG.alpha += Time.deltaTime * speed;
			yield return null;
		}
		fadeCG.alpha = 1;
		SetLevelText(GameManager.Instance.CurrentLevelIndex+1);
		yield return new WaitForSeconds(1f);
		while (fadeCG.alpha > 0)
		{
			fadeCG.alpha -= Time.deltaTime * speed;
			yield return null;
		}
		fadeCG.alpha = 0;
	}


	public IEnumerator DoFadeUIScreen(UIScreen screen, float speed = 1f)
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
		StartCoroutine(DoFadeUIScreen(gameOverScreen, 1));
    }
	public void DisplayCreditsScreen()
	{
		UIScreen.Focus(creditsScreen);
		StartCoroutine(DoFadeUIScreen(creditsScreen, 1));
	}
}
