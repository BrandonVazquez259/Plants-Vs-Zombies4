using UnityEngine;
using System.Collections;

public class Gun : BasePlant
{
    [Header("Gun Components")]
    [SerializeField]
    private GunData gunData;
    [SerializeField]
    private InstantiatePoolObjects bulletPool;
    [SerializeField]
    private Transform bulletPivot;
    [SerializeField]
    private LayerMask enemiesLayer;
    [SerializeField]
    private float raycastOffset = 2f; 
    private bool isShooting = false;
    private Health enemyHealth;
    private Coroutine shootCoroutine;
    private void OnEnable()
    {
        enemyHealth = null;
        isShooting = false;
        IsActive = false;
        health.InitializeHealth(gunData.maxHealth);
        animator.Play(gunData.GetAnimationName(ActionKey.Idle), 0, 0f);
        SoundManager.instance.Play(gunData.GetSoundName(ActionKey.Appear));
    }

    private void Update()
    {
        if (isActive && !isShooting && health.CurrentHealth > 0)
        {
            Vector3 right = transform.TransformDirection(Vector3.right);
            Vector3 rayOrigin = transform.position + Vector3.up * raycastOffset;
            if (Physics.Raycast(rayOrigin, right, out RaycastHit hit, gunData.range, enemiesLayer))
            {
                isShooting = true;
                enemyHealth = hit.collider.GetComponent<Health>();
                shootCoroutine = StartCoroutine(ShootRoutine());
            }
            Debug.DrawRay(rayOrigin, right * gunData.range, Color.blue);
        }
    }

    private IEnumerator ShootRoutine()
    {
        while (enemyHealth && enemyHealth.CurrentHealth > 0)
        {
            yield return new WaitForSeconds(gunData.fireRate);
            animator.Play(gunData.GetAnimationName(ActionKey.Attack), 0, 0f);
            bulletPool.InstantiateObject(bulletPivot);
            SoundManager.instance.Play(gunData.GetSoundName(ActionKey.Attack));
        }
        isShooting = false;
        enemyHealth = null;
    }

    public void Die()
    {
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
        }
        currentStep.IsOccupied = false;
        currentStep = null;
        isShooting = false;
        enemyHealth = null;
        SoundManager.instance.Play(gunData.GetSoundName(ActionKey.Die));
        StartCoroutine(DieRoutine(gunData.GetAnimationName(ActionKey.Die)));
    }
}
