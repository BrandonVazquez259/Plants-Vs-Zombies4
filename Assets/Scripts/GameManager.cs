using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
[SerializeField]
private GameData gameData;
[SerializeField]
private EnemyManager enemyManager;
[SerializeField]
private UnityEvent onWinGame;
[SerializeField]
private UnityEvent onLoseGame;
public void StartLevel()
    {
        LevelData currentLevel = gameData.levels[gameData.currentLevelIndex];
        enemyManager.SetEnemiesToSpawn(currentLevel.enemiesToSpawn);
        enemyManager.StartSpawningEnemies();
    }
    public void WinGame()
    {
        onWinGame?.Invoke();
    }
    public void LoseGame()
    {
        onLoseGame?.Invoke();
    }
}
