using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    public int currentLevelIndex = 0;
    public List<LevelData> levels;

    public void SetLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;
    }
}
