using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private string appearAnimationName = "CoinAppear";
    [SerializeField]
    private string hideAnimationName = "HideCoin";
    [SerializeField]
    private float secondsActive = 5f;
    private Coroutine hideCoroutine;
    private Collider collider;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }
    private void OnEnable()
    {
        animator.Play(appearAnimationName);
        collider.enabled = true;
        hideCoroutine = StartCoroutine(HideAfterSeconds());
    }
    public void Collect()
    {
        collider.enabled = false;
        StartCoroutine(HideCoin());
    }
    private IEnumerator HideAfterSeconds()
    {
        yield return new WaitForSeconds(secondsActive);
        StartCoroutine(HideCoin());
    }
    private IEnumerator HideCoin()
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }
        animator.Play(hideAnimationName);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }


}
