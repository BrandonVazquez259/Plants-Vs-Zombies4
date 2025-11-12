using UnityEngine;
[System.Serializable]
public class ActionAssets
{
    public string soundName;

    public string animationName;
    public ActionKey actionKey;

}
public enum ActionKey
{
    Idle,
    Walk,
    Attack,
    Hit,
    Appear,
    Die,
Win,
}