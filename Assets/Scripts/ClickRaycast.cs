using UnityEngine;
using UnityEngine.Events;

public class ClickRaycast : MonoBehaviour
{
    [SerializeField]
    private float raycastDistance = 10f;
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private string coinTag = "Coin";
      [SerializeField]
    private string coinTag2 = "Coin";
    [SerializeField]
    private UnityEvent<Transform> onCoinCollected;
    [SerializeField]
    private UnityEvent<Transform> onCoinCollected2;
    
    [SerializeField]
    private bool isActive = true;
    public void SetActive(bool active)
    {
        isActive = active;
    }
        void Update()
        {
            if (isActive && Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, raycastDistance, layerMask))
                {
                if (hitInfo.collider.CompareTag(coinTag))
                {
                    PressCoin(hitInfo.collider.gameObject);
                }
                else 
                      if (hitInfo.collider.CompareTag(coinTag2))
                {
                    PressCoin2(hitInfo.collider.gameObject);
                }
                
                }
            }
        }

    private void PressCoin(GameObject coin)
    {
        onCoinCollected.Invoke(coin.transform);
        coin.GetComponent<Coin>().Collect();
    }
    private void PressCoin2(GameObject coin)
    {
        onCoinCollected.Invoke(coin.transform);
        coin.GetComponent<Coin>().Collect();
    }
}
