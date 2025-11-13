using UnityEngine;
using UnityEngine.Events;

public class GunCreator : MonoBehaviour
{
    [SerializeField]
    private float raycastDistance = 100f;
    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private string stepTag = "Step";
    private Transform objectToPlace;
    private bool objectPlaced = false;
    private Step currentStep;
    private void Update()
    {
        if (objectToPlace == null) return;
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, raycastDistance, targetLayer))
            {
                if (hitInfo.collider.CompareTag(stepTag))
                {
                    currentStep = hitInfo.collider.GetComponent<Step>();
                    objectToPlace.position = hitInfo.collider.transform.position;
                    objectPlaced = true;
                }
                else
                {
                    if (hitInfo.collider.CompareTag("Floor"))
                    {
                        objectToPlace.position = hitInfo.point;
                    }
                    objectPlaced = false;
                    currentStep = null;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!objectPlaced || currentStep == null || currentStep.IsOccupied)
            {
                objectToPlace.gameObject.SetActive(false);
               
            }
            else
            {
                BasePlant plant = objectToPlace.GetComponent<BasePlant>();
                plant.IsActive = true;
                plant.CurrentStep = currentStep;
                currentStep.IsOccupied = true;
             
            }
            objectToPlace = null;
        }
    }
    public void SetObjectToPlace(Transform objTransform)
    {
        Debug.Log("Agregar objeto");
        objectToPlace = objTransform;
        objectPlaced = false;
        currentStep = null;
    }
}
