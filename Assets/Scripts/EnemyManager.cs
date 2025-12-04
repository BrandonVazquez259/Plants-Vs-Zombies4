using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyManager : MonoBehaviour
{
[SerializeField]
private LaneManager laneManager;
[SerializeField]
private EnemyAssets[] enemyAssets;
[SerializeField]
private UnityEvent onWinGame;
private InstantiateEnemy[] enemiesToSpawn;
private List <Enemy> currentEnemies = new List<Enemy>();
private int totalEnemiesToSpawn;
private int totalEnemiesDead;
public void SetEnemiesToSpawn(InstantiateEnemy[] enemies)
    {
        enemiesToSpawn = enemies;
        totalEnemiesToSpawn = enemiesToSpawn.Length;
        totalEnemiesDead = 0;
        currentEnemies.Clear();
    }
    public void StartSpawningEnemies()
    {
        foreach (InstantiateEnemy enemyInfo in enemiesToSpawn)
        {
            StartCoroutine(SpawnEnemyWithDelay(enemyInfo));
        }
    }
    private IEnumerator SpawnEnemyWithDelay(InstantiateEnemy enemyInfo)
    {
        yield return new WaitForSeconds(enemyInfo.spawnTime);
        EnemyAssets enemy = Array.Find(enemyAssets, e => e.enemyType == enemyInfo.enemyType);
        if(enemy != null)
        {
            Lane lane = laneManager.Lanes[enemyInfo.laneIndex];
            InstantiatePoolObjects enemyPool = enemy.enemyPool;
            enemyPool.InstantiateObject(lane.EnemySpawnPoint);
            Enemy spawnedEnemy = enemyPool.GetCurrentObject().GetComponent<Enemy>();
            spawnedEnemy.OnDie.AddListener(OnEnemyDie);
            currentEnemies.Add(spawnedEnemy);
        }
    }
    private void OnEnemyDie()
    {
        totalEnemiesDead++;
        if(totalEnemiesDead >= totalEnemiesToSpawn)
        {
            foreach (Enemy enemy in currentEnemies)
            {
                enemy.OnDie.RemoveListener(OnEnemyDie);
            }
            onWinGame?.Invoke();
        }
    }
    public void EnemiesWin()
    {
        foreach(Enemy enemy in currentEnemies)
        {
         enemy.OnDie.RemoveListener(OnEnemyDie);
         enemy.Win();
        }
    }
}
