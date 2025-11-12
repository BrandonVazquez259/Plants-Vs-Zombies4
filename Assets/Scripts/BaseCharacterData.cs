using UnityEngine;

public class BaseCharacterData : ScriptableObject
{
    [Header("Common Settings")]
    public float maxHealth;
    public ActionAssets[] actionAssets;
    public string GetAnimationName(ActionKey actionKey)
    {
        foreach (var actionAssets in actionAssets)
        {
            if (actionAssets.actionKey == actionKey)
            {
                return actionAssets.animationName;
            }
        }
        return string.Empty;
    }
    public string GetSoundName (ActionKey actionKey)
    {
        foreach (var actionAssets in actionAssets)
        {
            if (actionAssets.actionKey == actionKey)
            {
                return actionAssets.soundName;
            }
        }
         return string.Empty;
    }
}
