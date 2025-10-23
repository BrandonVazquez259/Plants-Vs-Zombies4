using UnityEngine;
using System.Collections;
using UnityEngine.Events;
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
    [SerializeField]
    private float raycastOffset = 2f;
    [SerializeField]
    private UnityEvent<Transform> onAttackTarget;
    private bool isAttacking = false;
    private Coroutine attackCoroutine;
    private Health targetHealth;

    private void OnEnable()
    {
        health.InitializeHealth(enemyData.health);
        StartLooking();
        SoundManager.instance.Play("zombie_appear");
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
            Vector3 forward = transform.TransformDirection(Vector3.left);
            Vector3 rayOrigin = transform.position + Vector3.up * raycastOffset;
            if (Physics.Raycast(rayOrigin, forward, out RaycastHit hit, enemyData.attackRange, enemiesLayer))
            {
                isAttacking = true;
                targetHealth = hit.collider.GetComponent<Health>();
                attackCoroutine = StartCoroutine(Attack());
            }
            Debug.DrawRay(rayOrigin, forward * enemyData.attackRange, Color.red);
        }
    }
    private IEnumerator Attack()
    {
        while (targetHealth.CurrentHealth > 0)
        {
            SoundManager.instance.Play("zombie_attack");
            animator.Play(enemyData.attackAnimation, 0, 0f);
            yield return new WaitForSeconds(enemyData.attackDuration);
            SoundManager.instance.Play("hit_object");
            onAttackTarget?.Invoke(targetHealth.transform);
            targetHealth.TakeDamage(enemyData.damage);
            yield return new WaitForSeconds(enemyData.timeBetweenAttacks);
        }
        attackCoroutine = null;
        StartLooking();
    }
    public void Die()
    {
        SoundManager.instance.Play("zombie_dsie");
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
