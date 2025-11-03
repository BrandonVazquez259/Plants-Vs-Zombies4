using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public float maxHealth = 100f;
    public float fireRate = 0.5f;
    public float range = 15f;
    public string shootAnimationName = "shoot";
    public string idleAnimationName = "Idle";
    public string dieAnimationName = "Die";
    public string shootSoundName = "gun_shoot";
    public string dieShootName = "gun_die";
    public string appearSoundName = "gun_appear";
}
