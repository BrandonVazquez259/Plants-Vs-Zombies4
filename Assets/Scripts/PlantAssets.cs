using UnityEngine;

[System.Serializable]

public class PlantAssets
{
  public PlantType plantType;

  public GameObject plantButton;   
}

public enum PlantType
{
    Shield,
    Gun,
    HeavyGun,
    Flower,
} 
