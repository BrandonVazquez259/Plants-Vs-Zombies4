using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private string appearAnimationName = "CoinAppear";
    [SerializeField]
    private string hideAnimationName = "HideCoin";
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
    }
    public void Collect()
    {
        collider.enabled = false;
        StartCoroutine(HideCoin());
    }
    private IEnumerator HideCoin()
    {
        animator.Play(hideAnimationName);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }


}
