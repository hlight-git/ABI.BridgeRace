using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : AbstractMonoSingleton<LevelManager>
{
    [SerializeField] LevelDataset levelDataset;
    private GameObject currentLevel;
    private NavMeshDataInstance currentNavMeshData;
    private int currentLevelCount;

    public delegate void GameStartDelegate(Player player);
    public delegate void GameOverDelegate(Character winner);
    public event GameStartDelegate OnGameStartEvents;
    public event GameOverDelegate OnGameOverEvents;

    private Player player;

    private void Awake()
    {
        currentLevelCount = -1;
    }

    public void LoadNextLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
            currentNavMeshData.Remove();
        }
        if (currentLevelCount < levelDataset.levels.Count - 1)
        {
            currentLevelCount++;
        }
        currentNavMeshData = NavMesh.AddNavMeshData(levelDataset.levels[currentLevelCount].NavMeshData, Vector3.zero, Quaternion.identity);
        currentLevel = Instantiate(levelDataset.levels[currentLevelCount].LevelPrefab, Vector3.zero, Quaternion.identity);
        StartGame();
    }
    public void RestartLevel()
    {
        Destroy(currentLevel);
        currentLevel = Instantiate(levelDataset.levels[currentLevelCount].LevelPrefab, Vector3.zero, Quaternion.identity);
        StartGame();
    }
    public void BreakLevel()
    {
        Destroy(currentLevel);
        currentNavMeshData.Remove();
        currentLevelCount--;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void StartGame()
    {
        OnGameStartEvents?.Invoke(player);
    }
    public void GameOver(Character winner)
    {
        OnGameOverEvents?.Invoke(winner);
        if (winner == player)
        {
            UIManager.Instance.ShowWinPopUp(true);
        } else
        {
            UIManager.Instance.ShowLosePopUp(true);
        }
    }
}
