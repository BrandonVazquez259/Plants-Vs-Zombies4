using UnityEngine;
using System.Collections.Generic;

public class Lane : MonoBehaviour
{
    [SerializeField]
    private List<Transform> frames;
    [SerializeField]
    private Transform enemySpawnPoint;
    public List<Transform> Frames => frames;
    public Transform EnemySpawnPoint => enemySpawnPoint;
}
