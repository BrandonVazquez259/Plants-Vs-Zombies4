using UnityEngine;

[System.Serializable]
public class EnemyAssets
{
 public EnemyType enemyType; 
 public InstantiatePoolObjects enemyPool;  
}
[System.Serializable]
public class InstantiateEnemy
{
public float spawnTime;
public EnemyType enemyType;
public int laneIndex;
}
public enum EnemyType
{
    Basic,
    Strong,
}