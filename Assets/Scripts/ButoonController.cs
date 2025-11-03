using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButoonController : MonoBehaviour
{
    [SerializeField]
    public UnityEvent onButtonDown;
    [SerializeField]
    public UnityEvent onButtonUp;
    public void OnPointerDown(PointerEventData eventData)
    {
        onButtonDown?.Invoke();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        onButtonUp?.Invoke();
    }
}
