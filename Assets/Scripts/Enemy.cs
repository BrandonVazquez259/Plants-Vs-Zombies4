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
    private Collider collider;
    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        collider.enabled = true;
        health.InitializeHealth(enemyData.maxHealth);
        StartLooking();
      //  SoundManager.instance.Play("zombie_appear");
    }
    private void StartLooking()
    {
        isAttacking = false;
        animator.Play(enemyData.GetAnimationName(ActionKey.Walk));
    }
    private void Update()
    {
        if (!isAttacking && health.CurrentHealth > 0)
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
        while (targetHealth != null && targetHealth.CurrentHealth > 0)
        {
            SoundManager.instance.Play(enemyData.GetSoundName(ActionKey.Attack));
            animator.Play(enemyData.GetAnimationName(ActionKey.Attack), 0, 0f);
            yield return new WaitForSeconds(enemyData.attackDuration);
            SoundManager.instance.Play(enemyData.GetAnimationName(ActionKey.Hit));
            onAttackTarget?.Invoke(targetHealth.transform);
            targetHealth.TakeDamage(enemyData.damage);
            yield return new WaitForSeconds(enemyData.timeBetweenAttacks);
        }
        targetHealth = null;
        attackCoroutine = null;
        StartLooking();
    }
    public void Die()
    {
        collider.enabled = false;
        SoundManager.instance.Play(enemyData.GetSoundName(ActionKey.Die));
        StartCoroutine(DieRoutine());
    }
    private IEnumerator DieRoutine()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        animator.Play(enemyData.GetAnimationName(ActionKey.Die));
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
}
