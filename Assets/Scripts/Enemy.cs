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
    private UnityEvent onDie = new UnityEvent();
    public UnityEvent OnDie =>onDie;
    private bool isAttacking = false;
    private Coroutine attackCoroutine;
    private Health targetHealth;
    private Collider collider;

    private bool isActive = false;
    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        isActive = true;
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
        if (isActive && !isAttacking && health.CurrentHealth > 0)
        {
            transform.Translate(Vector3.left * enemyData.speed * Time.deltaTime);
            Vector3 forward = transform.TransformDirection(Vector3.left);
            Vector3 rayOrigin = transform.position + Vector3.up * raycastOffset;
            if (Physics.Raycast(rayOrigin, forward, out RaycastHit hit, enemyData.attackRange, enemiesLayer, QueryTriggerInteraction.Ignore))
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
        while (isActive && targetHealth != null && targetHealth.CurrentHealth > 0)
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
        isActive =false;
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
        onDie?.Invoke();
        gameObject.SetActive(false);
    }
    public void Win()
    {
       isActive = false;
       collider.enabled = false;
       if(attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        animator.Play(enemyData.GetAnimationName(ActionKey.Win));
        SoundManager.instance.Play(enemyData.GetSoundName(ActionKey.Win));
    }
}
