using System.Collections;
using UnityEngine;
 
public class BasePlant : MonoBehaviour
{
    [Header ("Common Commponents")]
    [SerializeField]
    protected Health health;
    public Health Health => health;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected Collider collider;
    protected Step currentStep;
    public Step CurrentStep
    {
        set { currentStep = value; }
    }
    protected bool isActive;
    public virtual bool IsActive
    {
        set
        {
            isActive = value;
            collider.enabled = value;
        }
    }
    protected  IEnumerator DieRoutine (string dieAnimationName)
    {
        IsActive = false;
        animator.Play(dieAnimationName, 0, 0f);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
}