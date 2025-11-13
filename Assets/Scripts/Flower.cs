using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Flower : BasePlant
{
    [SerializeField]
    private Collider stepsDetector;
    [SerializeField]
    private FlowerData flowerData;
    [SerializeField]
    private InstantiatePoolObjects coinPool;
    [SerializeField]
    private float coinsOffsetY = 0.5f;
    private List<Step> stepsInRange = new List<Step>();

    private Coroutine spawnCoinCoroutine;
    public override bool IsActive
    {
        set
        {
            base.IsActive = value;
            if (value)
            {
                stepsInRange.Clear();
            }
            stepsDetector.enabled = value;
            spawnCoinCoroutine = StartCoroutine(SpawnCoinRoutine());
        }
    }
    private void OnEnable()
    {
        //SoudManager.instance.Play(flowerData.GetSoundName(ActionKey.Appear));
        health.InitializeHealth(flowerData.maxHealth);
        animator.Play(flowerData.GetAnimationName(ActionKey.Idle), 0, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Step>(out Step step))
        {
            stepsInRange.Add(step);
        }
    }
    private IEnumerator SpawnCoinRoutine()
    {
        while (isActive && health.CurrentHealth > 0)
        {
            yield return new WaitForSeconds(flowerData.spawnCoinTime);
            animator.Play(flowerData.GetAnimationName(ActionKey.Attack), 0, 0f);
            SoundManager.instance.Play(flowerData.GetSoundName(ActionKey.Attack));
            for (int i = 0; i < flowerData.coinAmount; i++)
            {
                if (stepsInRange.Count > 0)
                {
                    Step randomStep = stepsInRange[Random.Range(0, stepsInRange.Count)];
                    Vector3 spawnPosition = randomStep.transform.position + Vector3.up * coinsOffsetY;
                    coinPool.InstantiateObject(spawnPosition);
                }
            }
        }
    }
    public void Die()
    {
        if (spawnCoinCoroutine != null)
        {
            StopCoroutine(spawnCoinCoroutine);
        }
        currentStep.IsOccupied = false;
        currentStep = null;
        StartCoroutine(DieRoutine(flowerData.GetAnimationName(ActionKey.Die)));
        SoundManager.instance.Play(flowerData.GetSoundName(ActionKey.Die));
    }
}
