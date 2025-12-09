using UnityEngine;
using System;

public class PlantManager : MonoBehaviour
{
[SerializeField]

private PlantAssets[] plantAssets;

public void SetAvaliablePlants(PlantType[] availablePlants)
{
    foreach(var plantAsset in plantAssets)
    {
     if(Array.Exists(availablePlants, plant => plant == plantAsset.plantType))
     {
        plantAsset.plantButton.SetActive(true);
     }
     else
     {
        plantAsset.plantButton.SetActive(false);
     }
    }
}
}
