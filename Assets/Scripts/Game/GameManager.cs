using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	// References

	[SerializeField] private CameraController _cameraController;
	public static CameraController CameraController => Instance._cameraController;

	[SerializeField] private Transform _world;
	public static Transform World => Instance._world;

	[SerializeField] private Level[] levels;

	// Level

	[field: SerializeField, ReadOnly] public int CurrentLevelIndex { get; private set; } = 0;
	[SerializeField, ReadOnly] private Level currentLevel = null;

	[field: SerializeField, ReadOnly] public bool IsPlayerDead { get; private set; } = false;

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
		Instance.NextLevel();
	}

	public void StartGame()
    {
		SpawnPlayer();
		LoadLevel();
    }

	public static void GameOver()
    {
		InterfaceManager.Instance.DisplayGameOver();
    }
	public static void CompleteGame()
    {

    }

	public static void ReloadGame()
    {
		SceneManager.LoadScene("Game");
    }


    #region Level Structure

	public void NextLevel()
    {
		if(CurrentLevelIndex >= levels.Length - 1)
        {
			CompleteGame();
        }
		else
        {
			CurrentLevelIndex++;
			LoadLevel();
		}
    }

	public void LoadLevel()
    {
		if (currentLevel) Destroy(currentLevel.gameObject);
		currentLevel = Instantiate(levels[CurrentLevelIndex], _world);
		SetPlayerToSpawn(currentLevel.startPoint);
	}

	public void SpawnPlayer()
    {

    }

	public void SetPlayerToSpawn(Transform spawnPoint)
    {
		//also use rotation
    }
	

    #endregion
}
