using UnityEngine;

public class Step : MonoBehaviour
{
    private bool isOccupied = false;
    public bool IsOccupied
    {
        get { return isOccupied; }
        set{ isOccupied = value; }
    }
}
