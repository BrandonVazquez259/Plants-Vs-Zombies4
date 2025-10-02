using UnityEngine;
using UnityEngine.Events;
using System.Collections;
public class CoinsSpawner : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<Vector3> onCoinsSpawned;
    [SerializeField]
    private LaneManager laneManager;
    [SerializeField]
    private float spawInterval = 3f;
    [SerializeField]
    private float offsetY = 0f;
    private Coroutine spawnCoroutine;
    private bool isActive = false;
    private void Start()
    {
        Active(true);
    }
    public void Active(bool active)
    {
        isActive = active;
        if (isActive)
        {
            spawnCoroutine = StartCoroutine(SpawnCoins());
        }
        else
        {
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
                spawnCoroutine = null;
            }
        }
    }
    private IEnumerator SpawnCoins()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(spawInterval);
            Transform frame = laneManager.GetFrameInLane();
            onCoinsSpawned?.Invoke(frame.position + Vector3.up * offsetY);
        }
    }
}
