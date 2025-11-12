using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : BaseCharacterData
{
    public float attackRange = 5f;
    public float timeBetweenAttacks = 2f;
    public float attackDuration = 1f;
    public float health = 100f;

    public float speed = 5f;

    public float damage = 10f;

    public string enemyName;


}
