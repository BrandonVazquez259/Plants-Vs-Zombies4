using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyObject : MonoBehaviour
{
    [SerializeField]
    private int cost;
    [SerializeField]
    private InstantiatePoolObjects objectsPool;
    [SerializeField]
    private CoinsMannager coinsManager;
    [SerializeField]
    private Text costText;
    [SerializeField]
    private UnityEvent<Transform> onObjectBought;
    private void Start()
    {
        costText.text = cost.ToString();
    }
         public void TryBuyObject()
    {
       if (coinsManager.CanBuy(cost))
       {
           objectsPool.InstantiateObject(transform);
           GameObject boughtObject = objectsPool.GetCurrentObject();
            onObjectBought?.Invoke(boughtObject.transform);
       }
    }
}
