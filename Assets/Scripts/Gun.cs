using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
  [SerializeField]
  private Health health;
  [SerializeField]
  private GunData gunData;
  [SerializeField]
  private InstantiatePoolObjects bulletPool;
  [SerializeField]
  private Transform bulletPivot;
  private Coroutine shootCoroutine;
  private void OnEnable()
  {
    health.InitializeHealth(gunData.maxHealth);
    //SoundManager.instance.Play(gunData.appearSoundName);
    shootCoroutine = StartCoroutine(ShootRoutine());
  }
  private IEnumerator ShootRoutine()
  {
  
    while (true)
    {
      yield return new WaitForSeconds(gunData.fireRate);
      bulletPool.InstantiateObject(bulletPivot);
      SoundManager.instance.Play(gunData.shootSoundName);
    }
  }
    public void Die()
    {
    if (shootCoroutine != null)
    {
      StopCoroutine(shootCoroutine);
    }
    SoundManager.instance.Play(gunData.dieShootName);
    gameObject.SetActive(false);
    }
}
