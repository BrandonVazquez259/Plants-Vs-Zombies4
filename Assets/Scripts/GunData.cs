using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : BaseCharacterData

{
[Header("Gun Settings")]
    public float fireRate = 0.5f;
    public float range = 15f;

}
