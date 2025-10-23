using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Health health;
  private void OnEnable()
    {
        health.InitializeHealth(100f);
    }
}
