using UnityEngine;
using UnityEngine.Events;
public class CoinsMannager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<string> onCoinsChanged;
    private int numberOfCoins = 0;

    public void SetNumberOfCoins(int amount)
    {
        numberOfCoins = amount;
        onCoinsChanged.Invoke(numberOfCoins.ToString());
    }
    public bool CanBuy(int cost)
    {
        if (numberOfCoins >= cost)
        {
            SetNumberOfCoins(numberOfCoins - cost);
            return true;
        }
        return false;
    }
    public void AddCoins(int amount)
    {
        SetNumberOfCoins(numberOfCoins + amount);
    }
}
