using UnityEngine;
using UnityEngine.Events;
public class House : MonoBehaviour
{
[SerializeField]
private string enemyTag = "Enemy";
[SerializeField]
private UnityEvent onLoseGame;
private void OnTriggerEnter(Collider other)
{
    if(other.CompareTag(enemyTag))
    {
        gameObject.GetComponent<Collider>().enabled = false;
        onLoseGame?.Invoke();
    }
}
}
