using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;
    [SerializeField]
    private Health health;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private LayerMask enemiesLayer;
    private bool isAttacking = false;
    private Coroutine attackCoroutine;
    private Health targetHealth;

    private void OnEnable()
    {
        health.InitializeHealth(enemyData.health);
        StartLooking();
    }
    private void StartLooking()
    {
        isAttacking = false;
        animator.Play(enemyData.walkAnimation);
    }
    private void Update()
    {
        if (!isAttacking)
        {
            transform.Translate(Vector3.left * enemyData.speed * Time.deltaTime);
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, forward, out RaycastHit hit, enemyData.attackRange, enemiesLayer))
            {
                isAttacking = true;
                targetHealth = hit.collider.GetComponent<Health>();
                attackCoroutine = StartCoroutine(Attack());
            }
            Debug.DrawRay(transform.position, forward * enemyData.attackRange, Color.red);
        }
    }
    private IEnumerator Attack()
    {
        while (targetHealth.CurrentHealth > 0)
        {
            animator.Play(enemyData.attackAnimation);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            targetHealth.TakeDamage(enemyData.damage);
            yield return new WaitForSeconds(enemyData.timeBetweenAttacks);
        }
        attackCoroutine = null;
        StartLooking();
    }
    public void Die()
    {
        StartCoroutine(DieRoutine());
    }
    private IEnumerator DieRoutine()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        animator.Play(enemyData.deathAnimation);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
}
