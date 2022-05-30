using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public Action AlarmRaised;
	public Action AlarmLowered;

	// References

	[SerializeField] private CameraController _cameraController;
	public static CameraController CameraController => Instance._cameraController;

	[SerializeField] private Transform _world;
	public static Transform World => Instance._world;

	[SerializeField] private Level[] levels;

	private GameObject playerCollectionObj;

	[field: SerializeField, ReadOnly] public int CurrentLevelIndex { get; private set; } = 0;
	[SerializeField, ReadOnly] private Level currentLevel = null;

	[field: SerializeField, ReadOnly] public bool IsPlayerDead { get; private set; } = false;

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

    public static void Goal()
	{
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
		Debug.Log("Completed Game!");
		InterfaceManager.Instance.EndGameFade(1);
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
			InterfaceManager.Instance.LevelTransitionFade(1);
			LoadLevel();
		}
    }

	public void LoadLevel()
    {
		StartCoroutine(LoadLevelRoutine(1));
	}

	public void SpawnPlayer()
    {
		playerCollectionObj = Instantiate(InterfaceManager.Instance.playerCollectionPrefab, World);
		CameraController.SetTarget(playerCollectionObj.GetComponentInChildren<GhostMovement>().transform);
    }

	public void SetPlayerToSpawn(Transform spawnPoint)
    {
		GhostMovement ghostMovement = FindObjectOfType<GhostMovement>();
		Debug.Assert(ghostMovement != null);
		ghostMovement.CC.enabled = false;
		ghostMovement.transform.position = spawnPoint.position + new Vector3(0,0.5f,0);
		playerCollectionObj.GetComponentInChildren <PossessionManager>().transform.position = spawnPoint.position + new Vector3(0, 0.5f, 0);
		playerCollectionObj.GetComponentInChildren<PossessionManager>().Start();
		ghostMovement.CC.enabled = true;
	}

	public IEnumerator LoadLevelRoutine(float length)
    {
		playerCollectionObj.SetActive(false);
		yield return new WaitForSeconds(length);
		if (currentLevel) Destroy(currentLevel.gameObject);
		playerCollectionObj.GetComponentInChildren<MovementManager>().SetCurrentPossessedMovable(null);
		currentLevel = Instantiate(levels[CurrentLevelIndex], _world);
		playerCollectionObj.SetActive(true);
		SetPlayerToSpawn(currentLevel.startPoint);
	}


	#endregion

	public void RaiseAlarm() {
		AlarmRaised?.Invoke();
	}

	public void LowerAlarm() {
		AlarmLowered?.Invoke();
	}
}
