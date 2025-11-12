
using UnityEngine;
 
public class Shield : BasePlant
{
    [SerializeField]
    private ShieldData shieldData;
    private void OnEnable()
    {
        isActive = false;
        health.InitializeHealth(shieldData.maxHealth);
        animator.Play(shieldData.GetAnimationName(ActionKey.Idle), 0, 0f);
        SoundManager.instance.Play(shieldData.GetAnimationName(ActionKey.Appear));
    }
 
    public void Hit()
    {
        animator.Play(shieldData.GetAnimationName(ActionKey.Hit), 0, 0f);
    }
 
    public void Die()
    {
        isActive = false;
        SoundManager.instance.Play(shieldData.GetSoundName(ActionKey.Die));
        StartCoroutine(DieRoutine(shieldData.GetAnimationName(ActionKey.Die)));
    }
}