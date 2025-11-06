
using UnityEngine;
 
public class Shield : BasePlant
{
    [SerializeField]
    private ShieldData shieldData;
    private void OnEnable()
    {
        isActive = false;
        health.InitializeHealth(shieldData.maxHealth);
        animator.Play(shieldData.idleAnimationName, 0, 0f);
        //SoundManager.instance.Play(shieldData.appearSoundName);
    }
 
    public void Hit()
    {
        animator.Play(shieldData.hitAnimationName, 0, 0f);
    }
 
    public void Die()
    {
        isActive = false;
       // SoundManager.instance.Play(shieldData.dieSoundName);
        StartCoroutine(DieRoutine(shieldData.dieAnimationName));
    }
}