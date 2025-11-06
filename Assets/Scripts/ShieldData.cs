using UnityEngine;
 
[CreateAssetMenu(fileName = "ShieldData", menuName = "Scriptable Objects/ShieldData")]
public class ShieldData : ScriptableObject
{
    public float maxHealth = 150f;
    public string idleAnimationName = "Idle";
    public string dieAnimationName = "Die";
    public string hitAnimationName = "Hit";
    public string appearSoundName = "shield_appear";
    public string dieSoundName = "shield_idle";
}